using Assets.UnityFoundation.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public static class DialogueEditorExtensions
    {
        public static Optional<DialogueNode> GetDialogueNodeBy(
            this DialogueEditor editor, Vector2 mousePosition
        )
        {
            var node = editor.SelectedDialogue
                .DialogueNodesValues
                .LastOrDefault(node => node.Rect.Contains(mousePosition));

            if(node == null) return Optional<DialogueNode>.None();

            return Optional<DialogueNode>.Some(node);
        }

        public static Vector2 GetMousePosition(this DialogueEditor editor)
        {
            return Event.current.mousePosition + editor.ScrollviewPosition;
        }
    }
}
