namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public interface IHasHealth
    {
        float BaseHealth { get; }
        float CurrentHealth { get; }
    }
}