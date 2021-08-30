using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    class MoveCanvasEvent : DialogueEditorEvent
    {
        public MoveCanvasEvent(DialogueEditor editor) : base(editor) { }

        public override bool IsActive(Event current) =>
            current.type == EventType.MouseDrag;

        public override void Handle()
        {
            editor.ScrollviewPosition += editor.lastMousePosition - Event.current.mousePosition;
            editor.lastMousePosition = Event.current.mousePosition;
            GUI.changed = true;
        }
    }
}
