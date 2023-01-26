using System;

namespace UnityFoundation.Code
{
    public interface IDependencyContainer
    {
        // Register
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
        void Register<TImplementation>();
        void Register<TConcrete>(TConcrete instance);
        void RegisterSingleton<TInterface, TConcrete>();

        // Post creation actions
        void RegisterAction<TInterface>(Action<TInterface> creationAction);

        // Setup
        void Setup<T>(IDependencySetup<T> instance);
        void Setup<T1, T2>(IDependencySetup<T1, T2> instance);

        // Instantiation
        TInterface Create<TInterface>();
        object Create(Type type);
    }
}
