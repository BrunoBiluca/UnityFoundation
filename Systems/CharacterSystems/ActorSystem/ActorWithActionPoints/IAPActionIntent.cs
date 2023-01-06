namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IAPActionIntent : IActionIntent
    {
        int ActionPointsCost { get; set; }
    }
}
