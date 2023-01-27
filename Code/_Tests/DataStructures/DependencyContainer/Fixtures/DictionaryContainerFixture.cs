using System;
using System.Collections.Generic;
using static UnityFoundation.Code.Tests.DependencyContainerTestsCases;

namespace UnityFoundation.Code.Tests
{
    public class DictionaryContainerFixture : IDependencyContainerFixture
    {
        private Dictionary<Type, IRegisteredType> types = new();

        public IDependencyContainerFixture WithConstant<T>(T instance)
        {
            types[typeof(T)] = new ConstantType(typeof(T), instance);
            return this;
        }

        public IDependencyContainerFixture SingleConstructor()
        {
            types[typeof(SingleConstructor)] = new DefaultConstructorType(typeof(SingleConstructor));
            return this;
        }

        public IDependencyContainerFixture WithDependencySetupInstance()
        {
            var dependencySetup = new DependencySetup();
            types[typeof(string)] = new ConstantType(typeof(string), "a");
            types[typeof(int)] = new ConstantType(typeof(int), 1);
            types[typeof(bool)] = new ConstantType(typeof(bool), true);
            types[typeof(SetupParameters)] = new DefaultConstructorType(typeof(SetupParameters));
            types[typeof(DependencySetup)] = new DependencySetupType(
                new ConstantType(typeof(DependencySetup), dependencySetup)
            );
            return this;
        }

        public IDependencyContainerFixture Full()
        {
            types[typeof(SingleConstructor)] = new DefaultConstructorType(typeof(SingleConstructor));
            types[typeof(IEmptyInterface)] = new DefaultConstructorType(typeof(NoConstructor));
            types[typeof(NoConstructor)] = new DefaultConstructorType(typeof(NoConstructor));
            types[typeof(string)] = new ConstantType(typeof(string), "a");
            types[typeof(int)] = new ConstantType(typeof(int), 1);
            types[typeof(bool)] = new ConstantType(typeof(bool), true);
            types[typeof(SetupParameters)] = new DefaultConstructorType(typeof(SetupParameters));
            types[typeof(DependencySetup)] = new DependencySetupType(
                new DefaultConstructorType(typeof(DependencySetup))
            );
            types[typeof(MultiDependencySetup)] = new DependencySetupType(
                new DefaultConstructorType(typeof(MultiDependencySetup))
            );
            types[typeof(DependencySetupRecursion)] = new DependencySetupType(
                new DefaultConstructorType(typeof(DependencySetupRecursion))
            );
            types[typeof(ISingletonInterface)] = new SingletonType(
                new DefaultConstructorType(typeof(SingletonClass))
            );
            types[typeof(Factory)] = new ProvideContainerType(
                new DefaultConstructorType(typeof(Factory))
            );
            return this;
        }

        public IDependencyContainer Build()
        {
            return new DependencyContainer(types);
        }
    }

    public class BinderContainerFixture : IDependencyContainerFixture
    {
        private readonly DependencyBinder binder = new();

        public IDependencyContainer Build()
        {
            return binder.Build();
        }

        public IDependencyContainerFixture Full()
        {
            binder.Register<SingleConstructor>();
            binder.Register<IEmptyInterface, NoConstructor>();
            binder.Register("a");
            binder.Register(1);
            binder.Register(true);
            binder.Register<DependencySetup>();
            binder.Register<MultiDependencySetup>();
            binder.Register<DependencySetupRecursion>();
            binder.RegisterSingleton<ISingletonInterface, SingletonClass>();
            binder.Register<Factory>();
            return this;
        }

        public IDependencyContainerFixture SingleConstructor()
        {
            binder.Register<SingleConstructor>();
            return this;
        }

        public IDependencyContainerFixture WithConstant<T>(T instance)
        {
            binder.Register(instance);
            return this;
        }

        public IDependencyContainerFixture WithDependencySetupInstance()
        {
            var dependencySetup = new DependencySetup();
            binder.Register("a");
            binder.Register(1);
            binder.Register(true);
            binder.Register(dependencySetup);
            return this;
        }
    }
}
