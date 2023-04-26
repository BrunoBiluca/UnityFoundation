using System;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class DialogueEditorFactory
    {
        private readonly DialogueEditor editor;

        public DialogueEditorFactory(DialogueEditor editor)
        {
            this.editor = editor;
        }

        public void Button(string text, IDialogueEditorAction action)
        {
            if(GUILayout.Button(text))
            {
                editor.Actions.Add(action);
            }
        }

        public void Button(string text, Action callback)
        {
            if(GUILayout.Button(text))
            {
                callback();
            }
        }

        public void RenderConnectionLineBottom(Rect parent, Rect childNode)
        {
            var parentPosition = new Vector2(parent.center.x, parent.yMax);
            var childPosition = new Vector2(childNode.center.x, childNode.yMin);
            var offset = new Vector2(0f, (childPosition.y - parentPosition.y) * .8f);

            Handles.DrawBezier(
                parentPosition,
                childPosition,
                parentPosition + offset,
                childPosition - offset,
                Color.white,
                null,
                4f
            );

            Handles.DrawAAConvexPolygon(
                childPosition + new Vector2(-5f, -2f),
                childPosition + new Vector2(5f, -2f),
                childPosition + new Vector2(0f, 3f)
            );
        }

        public void RenderConnectionLineRight(Rect parent, Rect childNode)
        {
            var parentPosition = new Vector2(parent.xMax, parent.center.y);
            var childPosition = new Vector2(childNode.xMin, childNode.center.y);
            var offset = new Vector2((childPosition.x - parentPosition.x) * .8f, 0f);

            Handles.DrawBezier(
                parentPosition,
                childPosition,
                parentPosition + offset,
                childPosition - offset,
                Color.white,
                null,
                4f
            );

            Handles.DrawAAConvexPolygon(
                childPosition + new Vector2(-2f, -5f),
                childPosition + new Vector2(-2f, 5f),
                childPosition + new Vector2(3f, 0f)
            );
        }
    }
}