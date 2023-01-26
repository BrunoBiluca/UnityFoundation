using System;
using System.Linq;

namespace UnityFoundation.Code
{
    public sealed class DefaulConstructorType : IRegisteredType
    {
        public Type ConcreteType { get; }
        private readonly IDependencyContainer container;

        public DefaulConstructorType(IDependencyContainer container, Type concreteType)
        {
            this.container = container;
            ConcreteType = concreteType;
        }

        public object Instantiate()
        {
            var defaultConstructor = ConcreteType.GetConstructors()[0];
            var defaultParams = defaultConstructor.GetParameters();
            var parameters = defaultParams
                .Select(param => container.Create(param.ParameterType))
                .ToArray();

            return defaultConstructor.Invoke(parameters);
        }
    }
}
