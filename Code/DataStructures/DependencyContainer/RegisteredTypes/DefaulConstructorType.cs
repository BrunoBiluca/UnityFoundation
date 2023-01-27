using System;
using System.Linq;

namespace UnityFoundation.Code
{

    public sealed class DefaultConstructorType : IRegisteredType
    {
        public Type ConcreteType { get; }

        public DefaultConstructorType(Type concreteType)
        {
            ConcreteType = concreteType;
        }

        public object Instantiate(IDependencyContainer container)
        {
            var constructors = ConcreteType.GetConstructors();
            if(constructors.Length == 0)
                throw new TypeWithNoDefaultConstructorException(ConcreteType);

            var defaultConstructor = ConcreteType.GetConstructors()[0];
            var defaultParams = defaultConstructor.GetParameters();
            var parameters = defaultParams
                .Select(param => container.Resolve(param.ParameterType))
                .ToArray();

            return defaultConstructor.Invoke(parameters);
        }
    }
}
