using System;

namespace UnityFoundation.Code
{
    public class FactoryInstantiator : IRegisteredType
    {
        private readonly Type factoryType;

        public Type ConcreteType { get; }

        public bool IsSingleton { get; set; }
        public object instance;

        public FactoryInstantiator(Type factoryType, Type concreteType)
        {
            this.factoryType = factoryType;
            ConcreteType = concreteType;
        }

        public object Instantiate(IDependencyContainer container)
        {
            var factory = container.Resolve(factoryType) as IDependencyFactory;

            if(IsSingleton && instance != null)
                return instance;

            instance = factory.Instantiate();
            return instance;
        }
    }
}
