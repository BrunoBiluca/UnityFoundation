namespace UnityFoundation.Code
{
    public interface IDependencyBinder
    {
        IDependencyContainer Build();
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
        void Register<TImplementation>();
        void Register<TConcrete>(TConcrete instance);
        void RegisterSingleton<TInterface, TConcrete>();
    }
}
