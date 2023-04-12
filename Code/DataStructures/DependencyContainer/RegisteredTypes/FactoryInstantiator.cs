using System;

namespace UnityFoundation.Code
{
    public class FactoryInstantiator : IRegisteredType
    {
        private readonly Type factoryType;

        public Type ConcreteType { get; }

        public FactoryInstantiator(Type factoryType, Type concreteType)
        {
            this.factoryType = factoryType;
            ConcreteType = concreteType;
        }

        public object Instantiate(IDependencyContainer container)
        {
            var factory = container.Resolve(factoryType) as IDependencyFactory;
            return factory.Instantiate();
        }
    }
}
