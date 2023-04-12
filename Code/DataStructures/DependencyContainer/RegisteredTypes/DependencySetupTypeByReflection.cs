using System;
using System.Linq;
using System.Reflection;

namespace UnityFoundation.Code
{
    public class DependencySetupTypeByReflection : IRegisteredType
    {
        private readonly IRegisteredType registeredType;
        private bool wasSetup;

        public Type ConcreteType => registeredType.ConcreteType;


        public DependencySetupTypeByReflection(IRegisteredType registeredType)
        {
            this.registeredType = registeredType;
        }

        public object Instantiate(IDependencyContainer container)
        {
            var instance = registeredType.Instantiate(container);

            if(wasSetup)
                return instance;

            wasSetup = true;

            var methods = ConcreteType.GetMethods().Where(m => m.Name == "Setup").ToList();
            if(methods.Count == 0)
                throw new TypeWithNoSetupMethodException(ConcreteType);

            var method = methods.First();
            var methodParameters = method.GetParameters()
                .Select(param => container.Resolve(param.ParameterType))
                .ToArray();
            method.Invoke(instance, methodParameters);

            return instance;
        }
    }
}
