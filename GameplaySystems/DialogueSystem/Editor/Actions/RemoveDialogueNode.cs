using Assets.UnityFoundation.DialogueSystem;
using System.Collections.Generic;
using UnityEditor;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class RemoveDialogueNode : IDialogueEditorAction
    {
        private readonly DialogueRepository repository;
        private readonly DialogueNode node;
        private DialogueEditor editor;

        public RemoveDialogueNode(DialogueRepository repository, DialogueNode node)
        {
            this.repository = repository;
            this.node = node;
        }

        public void SetDialogueEditor(DialogueEditor editor)
        {
            this.editor = editor;
        }

        public void Handle()
        {
            var objectsToUndo = new List<UnityEngine.Object> { repository.Dialogue, node };

            objectsToUndo.AddRange(repository.Dialogue.GetNextDialogueNodes(node));
            objectsToUndo.AddRange(repository.Dialogue.GetPreviousDialogueNodes(node));

            Undo.RecordObjects(objectsToUndo.ToArray(), "Remove dialogue node");

            if(editor.LinkingNodes.LinkingNode.Get() == node)
            {
                editor.LinkingNodes.Clear();
            }
            repository.RemoveNode(node);

            Undo.DestroyObjectImmediate(node);
        }
    }
}