using Assets.UnityFoundation.Code;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class DialogueNodeAreaComponent
    {
        private static Texture2D backgroundTex;

        private readonly DialogueEditor editor;
        private readonly DialogueEditorFactory guiFactory;
        private readonly DialogueEditorStyles styles;
        private readonly DialogueRepository repository;

        public DialogueNodeAreaComponent(
            DialogueEditor editor,
            DialogueEditorFactory guiFactory,
            DialogueEditorStyles styles,
            DialogueRepository repository
        )
        {
            this.editor = editor;
            this.guiFactory = guiFactory;
            this.styles = styles;
            this.repository = repository;

            if(backgroundTex == null)
                backgroundTex = Resources.Load<Texture2D>("background");
        }

        public void Render()
        {
            editor.ScrollviewPosition = GUILayout.BeginScrollView(editor.ScrollviewPosition);
            Vector2 scrollviewSize = editor.SelectedDialogue.GetViewSize();

            var canvasOffset = 400;
            var canvas = GUILayoutUtility.GetRect(
                scrollviewSize.x + canvasOffset, scrollviewSize.y + canvasOffset
            );

            GUI.DrawTextureWithTexCoords(
                canvas,
                backgroundTex,
                new Rect(
                    0,
                    (canvas.height / backgroundTex.height).Remainder(),
                    canvas.width / backgroundTex.width,
                    canvas.height / backgroundTex.height
                )
            );

            foreach(var node in editor.SelectedDialogue.DialogueNodesValues)
            {
                new DialogueNodeComponent(editor, guiFactory, styles, repository, node)
                    .Render();
            }
            GUILayout.EndScrollView();
        }
    }
}