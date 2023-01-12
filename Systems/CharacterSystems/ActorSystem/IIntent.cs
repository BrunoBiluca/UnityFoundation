namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IIntent
    {
        bool ExecuteImmediatly { get; }

        IAction Create();
    }
}
