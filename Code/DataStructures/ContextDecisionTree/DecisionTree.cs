using System;

namespace UnityFoundation.Code
{
    public abstract class DecisionTree<TContext>
    {
        private IDecisionHandler<TContext> rootHandler;
        protected TContext Context { get; set; }

        protected void SetRootHandler(IDecisionHandler<TContext> handler)
        {
            rootHandler = handler;
        }

        public void EvaluateDecisions(TContext context)
        {
            if(rootHandler == null)
                throw new InvalidOperationException("No root handler was setup.");

            if(context == null)
                throw new InvalidOperationException("No context was setup.");

            rootHandler.Decide(context);
        }

        public void EvaluateDecisions()
        {
            EvaluateDecisions(Context);
        }
    }
}