using UnityFoundation.Code;
using Assets.UnityFoundation.DialogueSystem;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class DialogueRepository
    {
        private readonly DialogueSO currentDialogue;

        public DialogueSO Dialogue => currentDialogue;

        public DialogueRepository(DialogueSO currentDialogue)
        {
            this.currentDialogue = currentDialogue;
        }

        public Optional<DialogueNode> Get(string dialogueNodeName)
        {
            return currentDialogue.Get(dialogueNodeName);
        }

        public DialogueNode CreateNode(DialogueNode newDialogueNode, DialogueNode parent)
        {
            currentDialogue.DialogueNodes.Add(newDialogueNode.name, newDialogueNode);

            if(parent != null)
            {
                newDialogueNode.Position = new Vector2(
                    parent.Rect.center.x,
                    parent.Rect.yMax + 10f
                );
                newDialogueNode.PreviousDialogueNodes.Add(parent.name);

                parent.NextDialogueNodes.Add(newDialogueNode.name);
            }

            return newDialogueNode;
        }

        public void RemoveNode(DialogueNode node)
        {
            currentDialogue.DialogueNodes.Remove(node.name);

            node.NextDialogueNodes.ForEach(nodeId => {
                if(currentDialogue.DialogueNodes.TryGetValue(nodeId, out DialogueNode next))
                {
                    next.PreviousDialogueNodes.Remove(node.name);
                }
            });

            node.PreviousDialogueNodes.ForEach(nodeId => {
                if(currentDialogue.DialogueNodes.TryGetValue(nodeId, out DialogueNode previous))
                {
                    previous.NextDialogueNodes.Remove(node.name);
                }
            });
        }

        public void UpdateStartDialogueNode(DialogueNode node)
        {
            currentDialogue.SetStartDialogueNode(node);
        }
    }
}