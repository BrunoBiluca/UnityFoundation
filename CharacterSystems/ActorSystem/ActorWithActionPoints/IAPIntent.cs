namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IAPIntent : IIntent
    {
        int ActionPointsCost { get; set; }
    }
}
