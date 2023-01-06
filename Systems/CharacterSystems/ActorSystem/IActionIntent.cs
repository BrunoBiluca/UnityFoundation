namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IActionIntent
    {
        bool ExecuteImmediatly { get; }

        IAction Create();
    }
}
