using UnityFoundation.Code;
using Assets.UnityFoundation.DialogueSystem;

namespace Assets.UnityFoundation.Systems.DialogueSystem.Editor
{
    public class LinkingNodeContainer
    {
        public Optional<DialogueNode> LinkingNode {
            get; private set;
        } = Optional<DialogueNode>.None();

        public Optional<DialogueNode> ParentNode {
            get; private set;
        } = Optional<DialogueNode>.None();

        public bool IsLinkingNodeSet { get { return LinkingNode.IsPresent; } }

        public void Clear()
        {
            LinkingNode = Optional<DialogueNode>.None();
            ParentNode = Optional<DialogueNode>.None();
        }

        public void SetLinkingNode(DialogueNode node)
        {
            LinkingNode = Optional<DialogueNode>.Some(node);
        }

        public void SetParentNode(DialogueNode node)
        {
            ParentNode = Optional<DialogueNode>.Some(node);
        }
    }
}