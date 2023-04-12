using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

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
            var parameters = GetParameters(container, defaultConstructor);
            return defaultConstructor.Invoke(parameters);
        }

        public object[] GetParameters(IDependencyContainer container, ConstructorInfo constructor)
        {
            var defaultParams = constructor.GetParameters();

            try
            {
                return defaultParams
                    .Select(param => container.Resolve(param.ParameterType))
                    .ToArray();
            }
            catch(TypeNotRegisteredException ex)
            {
                throw new ParameterNotRegisteredException(ConcreteType, ex.Type);
            }
        }
    }
}
