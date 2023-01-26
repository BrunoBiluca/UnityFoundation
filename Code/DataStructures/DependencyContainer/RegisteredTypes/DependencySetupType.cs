using System;
using System.Linq;

namespace UnityFoundation.Code
{
    public sealed class DependencySetupType : IRegisteredType
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

        private readonly IDependencyContainer container;
        private readonly IRegisteredType registeredType;

        public Type ConcreteType => registeredType.ConcreteType;

        public DependencySetupType(
            IDependencyContainer container,
            IRegisteredType registeredType
        )
        {
            this.container = container;
            this.registeredType = registeredType;
        }

        public object Instantiate()
        {
            var instance = registeredType.Instantiate();
            SetupDependencies(ref instance);
            return instance;
        }

        private void SetupDependencies(ref object instance)
        {
            var hasDependendySetup = ConcreteType
                .GetInterfaces()
                .Any(HasDependencySetup);

            if(hasDependendySetup)
            {
                // TODO: iterar apenas pelos métodos que implementam a interface IDependencySetup
                var allSetupMethods = ConcreteType
                    .GetMethods()
                    .Where(m => m.Name.Contains("Setup"));
                foreach(var method in allSetupMethods)
                {
                    var methodParameters = method.GetParameters()
                        .Select(param => container.Create(param.ParameterType))
                        .ToArray();
                    method.Invoke(instance, methodParameters);
                }
            }
        }
    }
}
