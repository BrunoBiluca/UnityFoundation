using System;

namespace UnityFoundation.Code
{
    public sealed class ProvideContainerType : IRegisteredType
    {
        private readonly IDependencyContainer container;
        private readonly IRegisteredType registeredType;

        public Type ConcreteType => registeredType.ConcreteType;

        public ProvideContainerType(IDependencyContainer container, IRegisteredType registeredType)
        {
            this.container = container;
            this.registeredType = registeredType;
        }

        public object Instantiate()
        {
            var instance = registeredType.Instantiate() as IContainerProvide;
            instance.Container = container;
            return instance;
        }
    }
}
