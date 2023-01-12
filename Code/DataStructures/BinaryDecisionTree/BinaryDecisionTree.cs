namespace UnityFoundation.Code
{
    public class BinaryDecisionTree
    {
        private readonly IDecision root;

        public BinaryDecisionTree(IDecision root)
        {
            this.root = root;
        }

        public void Execute()
        {
            root.Decide();
        }
    }
}
