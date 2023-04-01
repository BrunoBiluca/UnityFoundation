namespace UnityFoundation.Code
{

    public interface IBinaryDecision : IDecision
    {
        IBinaryDecision SetFailed(IBinaryDecision failed);
        IBinaryDecision SetFinal(IDecision final);
        IBinaryDecision SetNext(IBinaryDecision next);
    }
}
