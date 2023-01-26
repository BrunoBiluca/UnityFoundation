using System;

namespace UnityFoundation.Code
{
    public sealed class SingletonInstanceType : IRegisteredType
    {
        public Type ConcreteType => registeredType.ConcreteType;

        private object instance;
        private readonly IRegisteredType registeredType;

        public SingletonInstanceType(IRegisteredType registeredType)
        {
            this.registeredType = registeredType;
        }

        public object Instantiate()
        {
            instance ??= registeredType.Instantiate();
            return instance;
        }
    }
}
