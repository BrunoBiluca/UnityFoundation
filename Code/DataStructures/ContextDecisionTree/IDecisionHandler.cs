namespace UnityFoundation.Code
{
    public interface IDecisionHandler<TContext>
    {
        IDecisionHandler<TContext> SetNext(IDecisionHandler<TContext> decision);
        IDecisionHandler<TContext> SetFailed(IDecisionHandler<TContext> failed);
        void Decide(TContext context);
    }
}