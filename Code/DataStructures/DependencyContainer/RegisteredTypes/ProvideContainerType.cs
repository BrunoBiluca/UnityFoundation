using System;

namespace UnityFoundation.Code
{
    public sealed class ProvideContainerType : IRegisteredType
    {
        private readonly IRegisteredType registeredType;

        public Type ConcreteType => registeredType.ConcreteType;

        public ProvideContainerType(IRegisteredType registeredType)
        {
            this.registeredType = registeredType;
        }

        public object Instantiate(IDependencyContainer container)
        {
            var instance = registeredType.Instantiate(container) as IContainerProvide;
            instance.Container = container;
            return instance;
        }
    }
}
