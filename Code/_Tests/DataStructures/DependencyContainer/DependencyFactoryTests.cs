using NUnit.Framework;

namespace UnityFoundation.Code.Tests
{
    public class DependencyFactoryTests
    {
        [Test]
        public void Should_instantiate_using_registered_factory()
        {
            var binder = new DependencyBinder();
            binder.Register("test");
            binder.Register(123);
            binder.RegisterFactory<Factory, Instantiated>();

            var container = binder.Build();

            var obj = container.Resolve<Instantiated>();

            Assert.That(obj.Name, Is.EqualTo("test_123"));
        }

        [Test]
        public void Should_return_first_created_instance_registered()
        {
            var binder = new DependencyBinder();
            binder.Register("test");
            binder.Register(123);
            binder.RegisterFactory<Factory, Instantiated>(true);

            var container = binder.Build();

            var obj = container.Resolve<Instantiated>();
            var obj2 = container.Resolve<Instantiated>();

            Assert.That(obj, Is.SameAs(obj2));
        }


        [Test]
        public void Should_return_first_created_instance_registered_as_dependency()
        {
            var binder = new DependencyBinder();
            binder.Register("test");
            binder.Register(123);
            binder.RegisterSetup(new DependsOnInstantiated());
            binder.Register<AlsoDependsOnInstantiated>();
            binder.RegisterFactory<Factory, Instantiated>(true);

            var container = binder.Build();

            var depends1 = container.Resolve<DependsOnInstantiated>();
            var depends2 = container.Resolve<AlsoDependsOnInstantiated>();

            Assert.That(depends1.Instantiated, Is.SameAs(depends2.Instantiated));
            Assert.That(
                depends1.Instantiated.GetHashCode(),
                Is.EqualTo(depends2.Instantiated.GetHashCode())
            );
        }

        public class Instantiated
        {
            public string Name { get; }
            public Instantiated(string name) { Name = name; }
        }

        public class Factory : IDependencyFactory
        {
            private readonly string part1;
            private readonly int part2;

            public Factory(string part1, int part2)
            {
                this.part1 = part1;
                this.part2 = part2;
            }

            public object Instantiate()
            {
                return new Instantiated(part1 + "_" + part2);
            }
        }

        public class DependsOnInstantiated
        {
            public Instantiated Instantiated { get; set; }

            public void Setup(Instantiated instantiated)
            {
                Instantiated = instantiated;
            }
        }

        public class AlsoDependsOnInstantiated
        {
            public AlsoDependsOnInstantiated(Instantiated instantiated)
            {
                Instantiated = instantiated;
            }

            public Instantiated Instantiated { get; }
        }
    }
}
