using NUnit.Framework;
using static UnityFoundation.Code.Tests.DependencyContainerTestsCases;

namespace UnityFoundation.Code.Tests
{
    [TestFixture(typeof(DictionaryContainerFixture))]
    [TestFixture(typeof(BinderContainerFixture))]
    public class DependencyContainerTests<T> where T : IDependencyContainerFixture, new()
    {
        private T fixture;

        [SetUp]
        public void Setup()
        {
            fixture = new T();
        }

        [Test]
        public void Should_throw_exception_when_created_type_is_not_registered()
        {
            var container = fixture.Build();
            Assert.Throws<TypeNotRegisteredException>(() => container.Resolve<SingleConstructor>());
        }

        [Test]
        public void Should_throw_exception_when_any_dependency_is_not_registered()
        {
            var container = fixture.Build();
            Assert.Throws<TypeNotRegisteredException>(() => container.Resolve<SingleConstructor>());
        }

        [Test]
        public void Should_create_instance_when_all_types_are_registerered()
        {
            var container = fixture.Full().Build();

            var b = container.Resolve<SingleConstructor>();

            Assert.That(b, Is.Not.Null);
            Assert.That(b.A, Is.Not.Null);
        }

        [Test]
        public void Should_not_set_object_when_not_register_interface()
        {
            var container = fixture.Full().Build();

            var a = container.Resolve<NoConstructor>();
            Assert.That(a.Property, Is.Null);

            var b = container.Resolve<SingleConstructor>();

            Assert.That(b, Is.Not.Null);
            Assert.That(b.A, Is.Not.Null);
            Assert.That(a.Property, Is.Null);
        }

        [Test]
        public void Should_set_object_when_register_interface()
        {
            var container = fixture.Full().Build();
            container.RegisterAction<ISetProperty>(obj => obj.Property = "test_property");

            var a = container.Resolve<NoConstructor>();
            Assert.That(a.Property, Is.EqualTo("test_property"));

            var b = container.Resolve<SingleConstructor>();

            Assert.That(b, Is.Not.Null);
            Assert.That(b.A, Is.Not.Null);
            Assert.That(b.Property, Is.EqualTo("test_property"));
        }

        [Test]
        public void Should_setup_parameter_given_object_has_dependency_setup_interface()
        {
            var container = fixture.Full().Build();

            var c = container.Resolve<DependencySetup>();

            Assert.That(c.P1, Is.EqualTo("a"));
            Assert.That(c.P2, Is.EqualTo(1));
            Assert.That(c.P3, Is.EqualTo(true));
        }

        [Test]
        public void Should_setup_even_if_instance_was_not_created_by_the_container()
        {
            var container = fixture.Full().Build();

            var c = new DependencySetup();
            container.Setup(c);

            Assert.That(c.P1, Is.EqualTo("a"));
            Assert.That(c.P2, Is.EqualTo(1));
            Assert.That(c.P3, Is.EqualTo(true));
        }

        [Test]
        public void Should_setup_calling_only_method_that_implements_dependency_setup_interface()
        {
            var container = fixture.Full().Build();

            var c = container.Resolve<MultiDependencySetup>();

            Assert.That(c.Other, Is.Null);
        }

        [Test]
        public void Should_setup_multiple_parameters_given_object_has_dependency_setup_interface()
        {
            var container = fixture.Full().Build();

            var d = container.Resolve<MultiDependencySetup>();

            Assert.That(d.P1, Is.EqualTo("a"));
            Assert.That(d.P2, Is.EqualTo(1));
        }

        [Test]
        public void Should_singleton_instance_when_registered()
        {
            var container = fixture.Full().Build();

            var obj1 = container.Resolve<ISingletonInterface>();
            var obj2 = container.Resolve<ISingletonInterface>();

            Assert.That(obj1, Is.Not.Null);
            Assert.That(obj2, Is.Not.Null);
            Assert.That(obj1, Is.EqualTo(obj2));
        }

        [Test]
        public void Should_instantiate_with_multiple_dependency_setup_recursion()
        {
            var container = fixture.Full().Build();

            var instance = container.Resolve<DependencySetupRecursion>();

            Assert.That(instance, Is.Not.Null);
            Assert.That(instance.Dependency, Is.Not.Null);
            Assert.That(instance.Dependency2, Is.Not.Null);

            Assert.That(instance.Dependency.P1, Is.EqualTo("a"));
            Assert.That(instance.Dependency.P2, Is.EqualTo(1));
            Assert.That(instance.Dependency.P3, Is.EqualTo(true));

            Assert.That(instance.Dependency2.P1, Is.EqualTo("a"));
            Assert.That(instance.Dependency2.P2, Is.EqualTo(1));
        }

        [Test]
        public void Should_provide_dependency_container_when_class_requests()
        {
            var container = fixture.Full().Build();
            var factory = container.Resolve<Factory>();
            Assert.That(factory.Instantiate(), Is.EqualTo("a"));
        }

        [Test]
        public void Should_resolve_constant_instance_with_setup()
        {
            var container = fixture.WithDependencySetupInstance().Build();

            var instance = container.Resolve<DependencySetup>();

            Assert.That(instance.P1, Is.EqualTo("a"));
            Assert.That(instance.P2, Is.EqualTo(1));
            Assert.That(instance.P3, Is.EqualTo(true));
        }
    }
}
