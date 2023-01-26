using System;

namespace UnityFoundation.Code
{
    public sealed class ConstantInstanceType : IRegisteredType
    {
        private readonly object instance;

        public Type ConcreteType { get; }

        public ConstantInstanceType(Type concreteType, object instance)
        {
            ConcreteType = concreteType;
            this.instance = instance;
        }

        public object Instantiate()
        {
            return instance;
        }
    }
}
