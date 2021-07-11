using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Assets.UnityFoundation.EditorInspector;

namespace Assets.UnityFoundation.DialogueSystem
{
    [Serializable]
    public class DialogueDictionary : SerializableDictionary<string, DialogueNode> { }

    [CreateAssetMenu(fileName = "New dialogue", menuName = "Dialogue/Dialogue")]
    public class DialogueSO : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private DialogueDictionary dialogueNodes;
        [SerializeField] private string startDialogueNodeName;

        public DialogueDictionary DialogueNodes => dialogueNodes;
        public IEnumerable<DialogueNode> DialogueNodesValues => dialogueNodes.Values;
        public DialogueNode StartDialogueNode => dialogueNodes[startDialogueNodeName];

#if UNITY_EDITOR
        private void Awake()
        {
            if(dialogueNodes != null) return;
            dialogueNodes = new DialogueDictionary();
            var dialogueNode = CreateInstance<DialogueNode>().Setup();
            dialogueNodes.Add(dialogueNode.name, dialogueNode);
            startDialogueNodeName = dialogueNode.name;
        }
#endif

        public void SetStartDialogueNode(DialogueNode dialogueNode)
        {
            startDialogueNodeName = dialogueNode.name;
        }

        public void Clear()
        {
            startDialogueNodeName = null;

            foreach(var node in dialogueNodes.Values)
            {
                AssetDatabase.RemoveObjectFromAsset(node);
            }

            dialogueNodes = new DialogueDictionary();
        }

        public Optional<DialogueNode> Get(string dialogueNodeName)
        {
            if(dialogueNodes.TryGetValue(dialogueNodeName, out DialogueNode dialogueNode))
                return Optional<DialogueNode>.Some(dialogueNode);

            return Optional<DialogueNode>.None();
        }

        public IEnumerable<DialogueNode> GetNextDialogueNodes(DialogueNode node)
        {
            if(node.NextDialogueNodes == null) yield return null;

            foreach(var childId in node.NextDialogueNodes)
            {
                if(dialogueNodes.TryGetValue(childId, out DialogueNode childNode))
                {
                    yield return childNode;
                }
            }
        }

        public IEnumerable<DialogueNode> GetPreviousDialogueNodes(DialogueNode node)
        {
            if(node.PreviousDialogueNodes == null) yield return null;

            foreach(var nodeId in node.PreviousDialogueNodes)
            {
                if(dialogueNodes.TryGetValue(nodeId, out DialogueNode dialogueNode))
                {
                    yield return dialogueNode;
                }
            }
        }

        public bool IsStartLine(DialogueNode node)
        {
            return node.name == startDialogueNodeName;
        }

        public Vector2 GetViewSize()
        {
            if(dialogueNodes.Count == 0) return Vector2.zero;

            return new Vector2(
                dialogueNodes.Max(node => node.Value.Rect.x),
                dialogueNodes.Max(node => node.Value.Rect.y)
            );
        }

        public void OnBeforeSerialize()
        {
            if(string.IsNullOrEmpty(AssetDatabase.GetAssetPath(this)))
                return;

            foreach(var node in dialogueNodes)
            {
                if(!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(node.Value)))
                    continue;

                AssetDatabase.AddObjectToAsset(node.Value, this);
            }
        }

        public void OnAfterDeserialize()
        {
        }
    }

}
