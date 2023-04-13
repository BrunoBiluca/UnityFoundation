namespace UnityFoundation.Code.Tests
{
    public class DependencyContainerTestsCases
    {
        public interface ISingletonInterface { }

        public class SingletonClass : ISingletonInterface { }

        public interface IEmptyInterface { }
        public class NoConstructor : IEmptyInterface, ISetProperty
        {
            public string Property { get; set; }
        }

        public interface ISetProperty
        {
            public string Property { get; set; }
        }

        public class SingleConstructor : ISetProperty
        {
            public SingleConstructor(IEmptyInterface a)
            {
                A = a;
            }

            public IEmptyInterface A { get; }
            public string Property { get; set; }
        }

        public class SetupParameters
        {
            public string P1 { get; private set; }
            public int P2 { get; private set; }
            public bool P3 { get; private set; }

            public SetupParameters(string p1, int p2, bool p3)
            {
                P1 = p1;
                P2 = p2;
                P3 = p3;
            }
        }

        public class DependencySetup : IDependencySetup<SetupParameters>
        {
            public string P1 { get; private set; }
            public int P2 { get; private set; }
            public bool P3 { get; private set; }

            public void Setup(SetupParameters parameters)
            {
                P1 = parameters.P1;
                P2 = parameters.P2;
                P3 = parameters.P3;
            }
        }

        public class MultiDependencySetup : IDependencySetup<string, int>
        {
            public string P1 { get; private set; }
            public int P2 { get; private set; }
            public void Setup(string p1, int p2)
            {
                P1 = p1;
                P2 = p2;
            }

            public string Other { get; private set; }

            public void OtherSetup(string other)
            {
                Other = other;
            }
        }

        public class DependencySetupRecursion
            : IDependencySetup<DependencySetup, MultiDependencySetup>
        {
            public DependencySetup Dependency { get; private set; }
            public MultiDependencySetup Dependency2 { get; private set; }

            public void Setup(DependencySetup p1, MultiDependencySetup p2)
            {
                Dependency = p1;
                Dependency2 = p2;
            }
        }

        public class Factory : IContainerProvide
        {
            public IDependencyContainer Container { private get; set; }

            public string Instantiate()
            {
                return Container.Resolve<string>();
            }
        }

        public class FactoryInstantiationNull : IDependencyFactory
        {
            public object Instantiate()
            {
                return null;
            }
        }
    }
}
