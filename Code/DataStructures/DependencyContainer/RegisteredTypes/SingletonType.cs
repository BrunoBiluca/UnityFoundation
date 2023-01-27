using System;

namespace UnityFoundation.Code
{
    public sealed class SingletonType : IRegisteredType
    {
        public Type ConcreteType => registeredType.ConcreteType;

        private object instance;
        private readonly IRegisteredType registeredType;

        public SingletonType(IRegisteredType registeredType)
        {
            this.registeredType = registeredType;
        }

        public object Instantiate(IDependencyContainer container)
        {
            instance ??= registeredType.Instantiate(container);
            return instance;
        }
    }
}
