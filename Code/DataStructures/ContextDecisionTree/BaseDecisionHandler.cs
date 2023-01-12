namespace UnityFoundation.Code
{
    public abstract class BaseDecisionHandler<TContext> : IDecisionHandler<TContext>
    {
        private IDecisionHandler<TContext> nextHandler;
        private IDecisionHandler<TContext> failedHandler;

        public IDecisionHandler<TContext> SetNext(IDecisionHandler<TContext> decisionHandler)
        {
            nextHandler = decisionHandler;
            return this;
        }

        public IDecisionHandler<TContext> SetFailed(IDecisionHandler<TContext> decisionHandler)
        {
            failedHandler = decisionHandler;
            return this;
        }

        public void Decide(TContext context)
        {
            if(OnDecide(context))
            {
                nextHandler?.Decide(context);
                return;
            }

            failedHandler?.Decide(context);
        }

        protected abstract bool OnDecide(TContext context);
    }
}