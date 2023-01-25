using System;

namespace UnityFoundation.Code
{
    public interface IDependencyContainer
    {
        TInterface Create<TInterface>();
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
        void Register<TImplementation>();
        void Register<TInterface>(Action<TInterface> creationAction);
    }
}
