using NUnit.Framework;

namespace UnityFoundation.Code.Tests
{
    public partial class DependencyContainerTests
    {
        [Test]
        public void Should_throw_exception_when_created_type_is_not_registered()
        {
            var container = new DependencyContainer();

            Assert.Throws<TypeNotRegisteredException>(() => container.Create<B>());
        }

        [Test]
        public void Should_throw_exception_when_any_dependency_is_not_registered()
        {
            var container = new DependencyContainer();
            container.Register<B>();

            Assert.Throws<TypeNotRegisteredException>(() => container.Create<B>());
        }

        [Test]
        public void Should_create_instance_of_registered_type()
        {
            var container = new DependencyContainer();
            container.Register<B>();
            container.Register<IA, A>();

            var b = container.Create<B>();

            Assert.That(b, Is.Not.Null);
            Assert.That(b.A, Is.Not.Null);
        }

        [Test]
        public void Should_set_object_of_registered_type()
        {
            var container = new DependencyContainer();
            container.Register<ISetProperty>(obj => obj.Property = "test_property");
            container.Register<B>();
            container.Register<IA, A>();

            var a = container.Create<A>();
            Assert.That(a.Property, Is.EqualTo("test_property"));

            var b = container.Create<B>();

            Assert.That(b, Is.Not.Null);
            Assert.That(b.A, Is.Not.Null);
            Assert.That(b.Property, Is.EqualTo("test_property"));
        }

        [Test]
        public void Should_setup_parameter_given_object_has_dependency_setup_interface()
        {
            var container = new DependencyContainer();
            container.Register("a");
            container.Register(1);
            container.Register(true);
            container.Register<C>();

            var c = container.Create<C>();

            Assert.That(c.P1, Is.EqualTo("a"));
            Assert.That(c.P2, Is.EqualTo(1));
            Assert.That(c.P3, Is.EqualTo(true));
        }

        [Test]
        public void Should_setup_even_object_was_not_created_by_the_container()
        {
            var container = new DependencyContainer();
            container.Register("a");
            container.Register(1);
            container.Register(true);

            var c = new C();
            container.Setup(c);

            Assert.That(c.P1, Is.EqualTo("a"));
            Assert.That(c.P2, Is.EqualTo(1));
            Assert.That(c.P3, Is.EqualTo(true));
        }

        [Test]
        public void Should_setup_multiple_parameters_given_object_has_dependency_setup_interface()
        {
            var container = new DependencyContainer();
            container.Register("a");
            container.Register(1);
            container.Register<D>();

            var d = container.Create<D>();

            Assert.That(d.P1, Is.EqualTo("a"));
            Assert.That(d.P2, Is.EqualTo(1));
        }
    }

    public partial class DependencyContainerTests
    {
        public interface IA { }
        public class A : IA, ISetProperty
        {
            public string Property { get; set; }
        }

        public interface ISetProperty
        {
            public string Property { get; set; }
        }

        public class B : ISetProperty
        {
            public B(IA a)
            {
                A = a;
            }

            public IA A { get; }
            public string Property { get; set; }
        }

        public class CSetup
        {
            public string P1 { get; private set; }
            public int P2 { get; private set; }
            public bool P3 { get; private set; }

            public CSetup(string p1, int p2, bool p3)
            {
                P1 = p1;
                P2 = p2;
                P3 = p3;
            }
        }

        public class C : IDependencySetup<CSetup>
        {
            public string P1 { get; private set; }
            public int P2 { get; private set; }
            public bool P3 { get; private set; }

            public void Setup(CSetup parameters)
            {
                P1 = parameters.P1;
                P2 = parameters.P2;
                P3 = parameters.P3;
            }
        }

        public class D : IDependencySetup<string, int>
        {
            public string P1 { get; private set; }
            public int P2 { get; private set; }
            public void Setup(string p1, int p2)
            {
                P1 = p1;
                P2 = p2;
            }
        }
    }
}
