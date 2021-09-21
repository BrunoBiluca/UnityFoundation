using Assets.UnityFoundation.Code.Common;
using System;
using System.Linq;

namespace Assets.UnityFoundation.DialogueSystem
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        private DialogueSO currentDialogue;
        private DialogueNode currentDialogueNode;

        public void Setup(DialogueSO dialogue)
        {
            currentDialogue = dialogue;
            currentDialogueNode = currentDialogue.StartDialogueNode;

            DialogueUI.Instance.Display(currentDialogueNode);

            DialogueUI.Instance.OnNextLine -= NextDialogueNode;
            DialogueUI.Instance.OnDialogueChoiceMade -= NextChoosenDialogueNode;
            DialogueUI.Instance.OnNextLine += NextDialogueNode;
            DialogueUI.Instance.OnDialogueChoiceMade += NextChoosenDialogueNode;
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

            DialogueUI.Instance.Display(nextNodes);
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