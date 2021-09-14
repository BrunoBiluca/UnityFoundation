using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public abstract class DialogueEditorEvent
    {
        protected readonly DialogueEditor editor;

        protected DialogueEditorEvent(DialogueEditor editor)
        {
            this.editor = editor;
        }

        public abstract bool IsActive(Event current);

        public abstract void Handle();
    }
}
