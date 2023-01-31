using System;
using System.Collections.Generic;
using static UnityFoundation.Code.Tests.DependencyContainerTestsCases;

namespace UnityFoundation.Code.Tests
{
    public class DictionaryContainerFixture : IDependencyContainerFixture
    {
        private RegistryTypes registry = new();

        public IDependencyContainerFixture WithConstant<T>(T instance)
        {
            registry.Add(RegistryTypeBuilder.WithConstant(typeof(T), instance));
            return this;
        }

        public IDependencyContainerFixture SingleConstructor()
        {
            registry.Add(
                RegistryTypeBuilder.WithDefaultConstructor(typeof(SingleConstructor))
            );
            return this;
        }

        public IDependencyContainerFixture WithDependencySetupInstance()
        {
            registry.Add(RegistryTypeBuilder.WithConstant(typeof(string), "a"));
            registry.Add(RegistryTypeBuilder.WithConstant(typeof(int), 1));
            registry.Add(RegistryTypeBuilder.WithConstant(typeof(bool), true));
            registry.Add(RegistryTypeBuilder.WithDefaultConstructor(typeof(SetupParameters)));
            registry.Add(RegistryTypeBuilder
                .WithConstant(typeof(DependencySetup), new DependencySetup())
                .AddDependencySetup()
            );
            return this;
        }

        public IDependencyContainerFixture Full()
        {
            registry.Add(RegistryTypeBuilder.WithDefaultConstructor(typeof(SingleConstructor)));
            registry.Add(RegistryTypeBuilder
                .WithDefaultConstructor(typeof(NoConstructor))
                .AsInterface(typeof(IEmptyInterface))
            );
            registry.Add(RegistryTypeBuilder.WithConstant(typeof(string), "a"));
            registry.Add(RegistryTypeBuilder.WithConstant(typeof(int), 1));
            registry.Add(RegistryTypeBuilder.WithConstant(typeof(bool), true));

            registry.Add(RegistryTypeBuilder.WithDefaultConstructor(typeof(SetupParameters)));
            registry.Add(RegistryTypeBuilder
                .WithDefaultConstructor(typeof(DependencySetup))
                .AddDependencySetup()
            );

            registry.Add(RegistryTypeBuilder
                .WithDefaultConstructor(typeof(MultiDependencySetup))
                .AddDependencySetup()
            );

            registry.Add(RegistryTypeBuilder
                .WithDefaultConstructor(typeof(DependencySetupRecursion))
                .AddDependencySetup()
            );

            registry.Add(RegistryTypeBuilder
                .WithDefaultConstructor(typeof(SingletonClass))
                .AsInterface(typeof(ISingletonInterface))
                .AsSingleton()
            );

            registry.Add(RegistryTypeBuilder
                .WithDefaultConstructor(typeof(Factory)).AddProvideContainer()
            );

            return this;
        }

        public IDependencyContainer Build()
        {
            return new DependencyContainer(registry);
        }
    }
}
