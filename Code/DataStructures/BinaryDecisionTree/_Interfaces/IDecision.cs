namespace UnityFoundation.Code
{
    public interface IDecision
    {
        bool WasSuccessful { get; }

        void Decide();
    }
}
