using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    class OpenContextMenuEvent : DialogueEditorEvent
    {
        public OpenContextMenuEvent(DialogueEditor editor) : base(editor) { }

        public override bool IsActive(Event current) =>
            current.type == EventType.ContextClick;

        public override void Handle()
        {
            editor.GetDialogueNodeBy(editor.GetMousePosition())
                .Some(node => editor.contextMenu.ShowNodeContextMenu(node))
                .OrElse(() => editor.contextMenu.Show());
        }
    }
}
