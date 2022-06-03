using Assets.UnityFoundation.DialogueSystem;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class DialogueEditorStyles
    {
        private readonly DialogueSO dialogue;

        public DialogueEditorStyles(DialogueSO dialogue)
        {
            this.dialogue = dialogue;
        }

        public GUIStyle GeneralNode {
            get {
                var style = new GUIStyle();
                style.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
                style.normal.textColor = Color.white;
                style.padding = new RectOffset(20, 20, 20, 20);
                style.border = new RectOffset(12, 12, 12, 12);
                return style;
            }
        }

        public GUIStyle FirstLine {
            get {
                var style = new GUIStyle();
                style.normal.background = EditorGUIUtility.Load("node3") as Texture2D;
                style.padding = new RectOffset(20, 20, 20, 20);
                style.border = new RectOffset(12, 12, 12, 12);
                return style;
            }
        }

        public GUIStyle FinishLine {
            get {
                var style = new GUIStyle();
                style.normal.background = EditorGUIUtility.Load("node6") as Texture2D;
                style.padding = new RectOffset(20, 20, 20, 20);
                style.border = new RectOffset(12, 12, 12, 12);
                return style;
            }
        }

        public GUIStyle GetNodeStyle(DialogueNode node)
        {
            if(node.NextDialogueNodes.Count == 0)
                return FinishLine;

            if(dialogue.IsStartLine(node))
                return FirstLine;

            return GeneralNode;
        }
    }
}