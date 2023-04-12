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
    }
}
