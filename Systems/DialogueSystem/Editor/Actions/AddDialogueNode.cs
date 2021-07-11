using Assets.UnityFoundation.DialogueSystem;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class AddDialogueNode : IDialogueEditorAction
    {
        private readonly DialogueRepository repository;
        private readonly DialogueNode parent;
        private Optional<Vector2> position = Optional<Vector2>.None();
        private DialogueEditor editor;

        public AddDialogueNode(DialogueRepository repository, DialogueNode parent)
        {
            this.repository = repository;
            this.parent = parent;
        }

        public AddDialogueNode SetCreatePosition(Vector2 position)
        {
            this.position = Optional<Vector2>.Some(position);
            return this;
        }

        public void SetDialogueEditor(DialogueEditor editor)
        {
            this.editor = editor;
        }

        public void Handle()
        {
            var newDialogueNode = ScriptableObject.CreateInstance<DialogueNode>().Setup();
            position.Some(pos => newDialogueNode.Position = pos);

            Undo.RecordObjects(
                new Object[] { repository.Dialogue, newDialogueNode, parent },
                "Add Dialogue Node"
            );

            repository.CreateNode(newDialogueNode, parent);

            EditorUtility.SetDirty(parent);
            EditorUtility.SetDirty(newDialogueNode);
        }
    }
}