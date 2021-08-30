using Assets.UnityFoundation.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    class OnDeselectDialogueNodeEvent : DialogueEditorEvent
    {
        public OnDeselectDialogueNodeEvent(DialogueEditor editor) : base(editor) { }

        public override bool IsActive(Event current) =>
            current.type == EventType.MouseUp;

        public override void Handle()
        {
            editor.draggingNode = Optional<DialogueNode>.None();
        }
    }
}
