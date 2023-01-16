namespace UnityFoundation.Code
{

    public class DecisionTree<TContext> : BaseDecisionTree<TContext>
    {
        private readonly TContext initialContext;

        public DecisionTree() { }
        public DecisionTree(TContext context)
        {
            initialContext = context;
        }

        public override TContext InitilizeContext()
        {
            return initialContext ?? default;
        }
    }
}