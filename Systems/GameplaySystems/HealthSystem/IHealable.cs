namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public interface IHealable : IHasHealth
    {
        void Heal(float amount);
        void HealFull();
    }
}