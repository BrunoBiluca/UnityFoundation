using Assets.UnityFoundation.Code.Common;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.UnityFoundation.DialogueSystem
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        [SerializeField] private DialogueUI dialogueUI;

        private DialogueSO currentDialogue;
        private DialogueNode currentDialogueNode;

        public void Setup(DialogueSO dialogue)
        {
            currentDialogue = dialogue;
            currentDialogueNode = currentDialogue.StartDialogueNode;

            FindDialogueUIReference();
            SetupDialogueUI();
        }
        private void FindDialogueUIReference()
        {
            if(dialogueUI != null) return;

            dialogueUI = (DialogueUI)FindObjectOfType(typeof(DialogueUI));
        }

        private void SetupDialogueUI()
        {
            if(dialogueUI == null)
            {
                Debug.LogError("Dialogue UI not found on scene.");
                return;
            }

            dialogueUI.Display(currentDialogueNode);

            dialogueUI.OnNextLine -= NextDialogueNode;
            dialogueUI.OnDialogueChoiceMade -= NextChoosenDialogueNode;
            dialogueUI.OnNextLine += NextDialogueNode;
            dialogueUI.OnDialogueChoiceMade += NextChoosenDialogueNode;
        }

        private void NextDialogueNode(object sender, EventArgs e)
        {
            var nextNodes = currentDialogue
                .GetNextDialogueNodes(currentDialogueNode)
                .ToArray();

            if(nextNodes.Length == 0)
                return;

            if(nextNodes.Length == 1)
                currentDialogueNode = nextNodes[0];

            dialogueUI.Display(nextNodes);
        }

        private void NextChoosenDialogueNode(object sender, DialogueChoiceEventArgs e)
        {
            currentDialogue.Get(e.DialogueNodeName)
                .Some(dialogueNode => {
                    currentDialogueNode = dialogueNode;
                    NextDialogueNode(sender, e);
                });
        }

    }
}