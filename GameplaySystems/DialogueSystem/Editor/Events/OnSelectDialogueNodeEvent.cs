using UnityEditor;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    class OnSelectDialogueNodeEvent : DialogueEditorEvent
    {
        public OnSelectDialogueNodeEvent(DialogueEditor editor) : base(editor) { }

        public override bool IsActive(Event current) =>
            current.type == EventType.MouseDown;

        public override void Handle()
        {
            var mousePosition = editor.GetMousePosition();
            editor.draggingNode = editor.GetDialogueNodeBy(mousePosition);
            editor.draggingNode
                .Some(dialogueNode => {
                    editor.draggingOffset = dialogueNode.Rect.position - mousePosition;
                    Selection.activeObject = dialogueNode;
                })
                .OrElse(() =>
                    Selection.activeObject = editor.SelectedDialogue
                );
        }
    }
}
