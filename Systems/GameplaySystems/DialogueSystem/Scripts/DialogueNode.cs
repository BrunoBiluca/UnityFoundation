using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.DialogueSystem
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] private SpearkerSO spearker;
        [SerializeField] private string text;
        [SerializeField] private List<string> nextDialogueNodes = new List<string>();
        [SerializeField] private List<string> previousDialogueNodes = new List<string>();
        [SerializeField] private Rect rect = new Rect(100, 100, 200, 100);

        public DialogueNode Setup()
        {
            name = Guid.NewGuid().ToString();
            text = "example";
            return this;
        }

        public SpearkerSO Spearker {
            get { return spearker; }
            set { spearker = value; }
        }

        public string Text {
            get { return text; }
            set { text = value; }
        }

        public List<string> NextDialogueNodes { get { return nextDialogueNodes; } }

        public List<string> PreviousDialogueNodes { get { return previousDialogueNodes; } }

        public Rect Rect { get { return rect; } set { rect = value; } }

        public Vector2 Position {
            get { return rect.position; }
            set { rect.position = value; }
        }

        public bool HasLink(DialogueNode node)
        {
            return !nextDialogueNodes.Contains(node.name)
                && !previousDialogueNodes.Contains(node.name);
        }

        public void Link(DialogueNode nextDialogueNode)
        {
            NextDialogueNodes.Add(nextDialogueNode.name);
            nextDialogueNode.PreviousDialogueNodes.Add(name);
        }

        public void Unlink(DialogueNode nextDialogueNode)
        {
            NextDialogueNodes.Remove(nextDialogueNode.name);
            nextDialogueNode?.PreviousDialogueNodes.Remove(name);
        }
    }
}