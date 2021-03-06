using Assets.UnityFoundation.DialogueSystem;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class ChangeDialogueNodeAction : IDialogueEditorAction
    {
        public static ChangeDialogueNodeAction create(DialogueNode node)
        {
            return new ChangeDialogueNodeAction(node);
        }

        private readonly DialogueNode node;
        private Optional<string> newText;
        private Optional<SpearkerSO> newSpeaker;
        private Optional<Vector2> newPosition;
        private DialogueEditor editor;

        public ChangeDialogueNodeAction(DialogueNode node)
        {
            this.node = node;

            newText = Optional<string>.None();
            newSpeaker = Optional<SpearkerSO>.None();
            newPosition = Optional<Vector2>.None();
        }

        public ChangeDialogueNodeAction SetText(string newText)
        {
            this.newText = Optional<string>.Some(newText);
            return this;
        }

        public ChangeDialogueNodeAction SetSpeaker(SpearkerSO spearker)
        {
            newSpeaker = Optional<SpearkerSO>.Some(spearker);
            return this;
        }

        public ChangeDialogueNodeAction SetPosition(Vector2 position)
        {
            newPosition = Optional<Vector2>.Some(position);
            return this;
        }

        public void SetDialogueEditor(DialogueEditor editor)
        {
            this.editor = editor;
        }

        public void Handle()
        {
            Undo.RecordObject(node, "Update dialogue node");

            newText.Some(text => node.Text = text);
            newSpeaker.Some(speaker => node.Spearker = speaker);
            newPosition.Some(position => node.Position = position);

            EditorUtility.SetDirty(node);

            editor.LinkingNodes.Clear();
        }
    }
}