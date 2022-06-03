using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class UpdateMousePositionEvent : DialogueEditorEvent
    {
        public UpdateMousePositionEvent(DialogueEditor editor) : base(editor) { }

        public override bool IsActive(Event current) =>
            current.type == EventType.MouseDown;

        public override void Handle()
        {
            editor.lastMousePosition = Event.current.mousePosition;
        }
    }
}
