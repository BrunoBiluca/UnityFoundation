using Assets.UnityFoundation.Code;
using Assets.UnityFoundation.Code.Common;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UnityFoundation.DialogueSystem
{
    public class DialogueChoiceEventArgs : EventArgs
    {
        public string DialogueNodeName { get; private set; }

        public DialogueChoiceEventArgs(string dialogueNodeName)
        {
            DialogueNodeName = dialogueNodeName;
        }
    }

    public class DialogueUI : Singleton<DialogueUI>
    {
        [SerializeField] private Transform optionPrefab;

        public event EventHandler OnNextLine;
        public event EventHandler<DialogueChoiceEventArgs> OnDialogueChoiceMade;

        private TextMeshProUGUI speakerText;

        private Transform simpleDialogue;
        private TextMeshProUGUI dialogueText;

        private Transform choicesDialogue;

        private Button nextButton;
        private Button closeButton;

        protected override void OnAwake()
        {
            speakerText = transform.Find("speaker_text").GetComponent<TextMeshProUGUI>();

            simpleDialogue = transform.Find("simple_dialogue");
            dialogueText = simpleDialogue.Find("dialogue_text").GetComponent<TextMeshProUGUI>();

            choicesDialogue = transform.Find("choices_dialogue");

            nextButton = transform
                .Find("simple_dialogue")
                .Find("next_button_holder")
                .Find("next_button")
                .GetComponent<Button>();
            nextButton.onClick.AddListener(() => OnNextLine?.Invoke(this, EventArgs.Empty));

            closeButton = transform.Find("close_button").GetComponent<Button>();
            closeButton.onClick.AddListener(() => gameObject.SetActive(false));

            gameObject.SetActive(false);
        }

        public void Display(params DialogueNode[] dialogueNodes)
        {
            gameObject.SetActive(true);

            DialogueNode mainDialogueNode = dialogueNodes[0];
            if(mainDialogueNode.Spearker == null)
                speakerText.text = "????";
            else
                speakerText.text = mainDialogueNode.Spearker.SpearkerName;

            if(dialogueNodes.Length == 1)
                SimpleDialogue(mainDialogueNode);
            else
                ChoicesDialogue(dialogueNodes);

        }

        private void SimpleDialogue(DialogueNode mainDialogueNode)
        {
            choicesDialogue.gameObject.SetActive(false);
            simpleDialogue.gameObject.SetActive(true);
            dialogueText.text = mainDialogueNode.Text;

            if(mainDialogueNode.NextDialogueNodes.Count > 0)
                nextButton.gameObject.SetActive(true);
            else
                nextButton.gameObject.SetActive(false);
        }
        private void ChoicesDialogue(DialogueNode[] dialogueNodes)
        {
            choicesDialogue.gameObject.SetActive(true);
            simpleDialogue.gameObject.SetActive(false);

            TransformUtils.RemoveChildObjects(choicesDialogue);

            foreach(var dialogueNode in dialogueNodes)
            {
                var go = Instantiate(optionPrefab, choicesDialogue);
                go.Find("option_text").GetComponent<TextMeshProUGUI>().text = dialogueNode.Text;

                go.GetComponent<Button>()
                    .onClick
                    .AddListener(() =>
                        OnDialogueChoiceMade?.Invoke(
                            this,
                            new DialogueChoiceEventArgs(dialogueNode.name)
                        )
                    );
            }
        }
    }

}
