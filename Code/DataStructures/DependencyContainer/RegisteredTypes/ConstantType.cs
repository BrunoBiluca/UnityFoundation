using System;

namespace UnityFoundation.Code
{
    public sealed class ConstantType : IRegisteredType
    {
        private readonly object instance;

        public Type ConcreteType { get; }

        public ConstantType(Type concreteType, object instance)
        {
            ConcreteType = concreteType;
            this.instance = instance;
        }

        public object Instantiate(IDependencyContainer container)
        {
            return instance;
        }
    }
}
