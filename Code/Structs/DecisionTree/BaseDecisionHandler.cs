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

        public void Handle(TContext context)
        {
            if(OnHandle(context))
            {
                if(nextHandler != null)
                    nextHandler.Handle(context);
                return;
            }

            if(failedHandler != null)
                failedHandler.Handle(context);
        }

        public abstract bool OnHandle(TContext context);
    }
}