using Assets.UnityFoundation.DialogueSystem;
using UnityEditor;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class LinkDialogueNodes : IDialogueEditorAction
    {
        public static LinkDialogueNodes Create(DialogueNode node)
        {
            return new LinkDialogueNodes(node);
        }

        private readonly DialogueNode parent;
        private DialogueEditor editor;

        public LinkDialogueNodes(DialogueNode node)
        {
            parent = node;
        }
        public void SetDialogueEditor(DialogueEditor editor)
        {
            this.editor = editor;
        }

        public void Handle()
        {
            var child = editor.LinkingNodes.LinkingNode.Get();

            Undo.RecordObjects(new UnityEngine.Object[] { child, parent }, "Link dialogue node");
            parent.Link(child);
            EditorUtility.SetDirty(parent);
            EditorUtility.SetDirty(child);

            editor.LinkingNodes.Clear();
        }
    }
}