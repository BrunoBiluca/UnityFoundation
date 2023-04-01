using System;
using System.Collections;
using System.Linq;

namespace UnityFoundation.Code
{

    public sealed class DependencySetupType : IRegisteredType
    {

        private readonly IRegisteredType registeredType;
        private readonly bool setupOnce;
        private bool wasSetup = false;

        public Type ConcreteType => registeredType.ConcreteType;

        public DependencySetupType(IRegisteredType registeredType, bool setupOnce = false)
        {
            this.registeredType = registeredType;
            this.setupOnce = setupOnce;
        }

        public object Instantiate(IDependencyContainer container)
        {
            var instance = registeredType.Instantiate(container);

            if(!setupOnce)
                SetupDependencies(container, ref instance);

            if(setupOnce && !wasSetup)
            {
                wasSetup = true;
                SetupDependencies(container, ref instance);
            }

            return instance;
        }

        private void SetupDependencies(IDependencyContainer container, ref object instance)
        {
            foreach(var method in DependencySetupValidation.GetMethods(ConcreteType))
            {
                var methodParameters = method.GetParameters()
                    .Select(param => container.Resolve(param.ParameterType))
                    .ToArray();
                method.Invoke(instance, methodParameters);
            }
        }
    }
}
