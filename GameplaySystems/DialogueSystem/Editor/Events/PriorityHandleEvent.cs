using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor.Events
{
    public class PriorityHandleEvent : DialogueEditorEvent
    {
        protected List<DialogueEditorEvent> editorEvents;

        public PriorityHandleEvent(DialogueEditor editor) : base(editor)
        {
            editorEvents = new List<DialogueEditorEvent>();
        }

        public PriorityHandleEvent AddEvent(DialogueEditorEvent editorEvent)
        {
            editorEvents.Add(editorEvent);
            return this;
        }

        public override bool IsActive(Event current) => 
            editorEvents.Any(e => e.IsActive(current));

        public override void Handle()
        {
            editorEvents.Find(e => e.IsActive(Event.current))?
                .Handle();
        }
    }
}
