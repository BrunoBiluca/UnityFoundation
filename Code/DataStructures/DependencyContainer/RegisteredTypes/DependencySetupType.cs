using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityFoundation.Code
{
    public static class DependencySetupValidation
    {
        public static bool HasDependencySetup(Type t)
        {
            return t.IsGenericType
              && (
                t.GetGenericTypeDefinition() == typeof(IDependencySetup<>)
                  || t.GetGenericTypeDefinition() == typeof(IDependencySetup<,>)
                  || t.GetGenericTypeDefinition() == typeof(IDependencySetup<,,>)
                  || t.GetGenericTypeDefinition() == typeof(IDependencySetup<,,,>)
              );
        }

        public static IEnumerable<MethodInfo> GetMethods(Type t)
        {
            var interfaces = t.GetInterfaces().Where(HasDependencySetup);

            foreach(var i in interfaces)
            {
                var methods = t.GetMethods()
                    .Where(m => m.ToString() == i.GetMethod("Setup").ToString());

                foreach(var method in methods)
                    yield return method;
            }
        }
    }

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
