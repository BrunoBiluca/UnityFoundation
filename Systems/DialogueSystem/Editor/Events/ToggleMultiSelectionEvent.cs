using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class ToggleMultiSelectionEvent : DialogueEditorEvent
    {
        public ToggleMultiSelectionEvent(DialogueEditor editor) : base(editor) { }

        public override bool IsActive(Event current) =>
            (
                current.type == EventType.KeyDown
                || current.type == EventType.KeyUp
            )
            && (
                current.keyCode == KeyCode.RightShift
                || current.keyCode == KeyCode.LeftShift
            );

        public override void Handle()
        {
            if(Event.current.type == EventType.KeyDown)
            {
                editor.multiSelection = true;
                return;
            }
                

            if(Event.current.type == EventType.KeyUp)
                editor.multiSelection = false;
        }
    }
}
