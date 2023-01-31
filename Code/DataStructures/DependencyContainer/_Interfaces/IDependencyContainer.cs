using System;

namespace UnityFoundation.Code
{
    public interface IDependencyContainer
    {
        // Post creation actions
        void RegisterAction<TInterface>(Action<TInterface> creationAction);

        // Setup
        void Setup<T>(IDependencySetup<T> instance);
        void Setup<T1, T2>(IDependencySetup<T1, T2> instance);
        void Setup<T1, T2, T3>(IDependencySetup<T1, T2, T3> instance);
        void Setup<T1, T2, T3, T4>(IDependencySetup<T1, T2, T3, T4> instance);

        // Instantiation
        TInterface Resolve<TInterface>();
        TInterface Resolve<TInterface>(Enum key);
        TInterface Resolve<TInterface>(params object[] parameters);
        object Resolve(Type type);
    }
}
