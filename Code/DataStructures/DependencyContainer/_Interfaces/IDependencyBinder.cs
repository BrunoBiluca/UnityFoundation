using System;

namespace UnityFoundation.Code
{
    public interface IDependencyBinder
    {
        IDependencyContainer Build();
        void Register<TInterface, TImplementation>(Enum key = null) 
            where TImplementation : TInterface;
        void Register<TImplementation>(Enum key = null);
        void Register<TConcrete>(TConcrete instance, Enum key = null);
        void RegisterFactory<TFactory, TInterface>(bool isSingleton = false);
        void RegisterModule(IDependencyModule module);
        void RegisterSetup<TConcrete>(TConcrete instance);
        void RegisterSingleton<TInterface, TConcrete>();
    }
}
