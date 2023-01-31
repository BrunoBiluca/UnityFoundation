using static UnityFoundation.Code.Tests.DependencyContainerTestsCases;

namespace UnityFoundation.Code.Tests
{
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
