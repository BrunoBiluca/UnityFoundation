using Assets.UnityFoundation.DialogueSystem;
using UnityEditor;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class UnlinkDialogueNodes : IDialogueEditorAction
    {
        public static UnlinkDialogueNodes Create(DialogueNode node)
        {
            return new UnlinkDialogueNodes(node);
        }

        private readonly DialogueNode parent;
        private DialogueEditor editor;

        public UnlinkDialogueNodes(DialogueNode node)
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

            Undo.RecordObjects(new UnityEngine.Object[] { child, parent }, "Unink dialogue node");
            parent.Unlink(child);

            EditorUtility.SetDirty(parent);
            EditorUtility.SetDirty(child);

            editor.LinkingNodes.Clear();
        }

    }
}