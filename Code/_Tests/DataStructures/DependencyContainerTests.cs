using NUnit.Framework;

namespace UnityFoundation.Code.Tests
{
    public partial class DependencyContainerTests
    {
        [Test]
        public void Should_throw_exception_when_created_type_is_not_registered()
        {
            var container = new DependencyContainer();

            Assert.Throws<TypeNotRegisteredException>(
                () => container.Create<ClassWithConstructor>()
            );
        }

        [Test]
        public void Should_throw_exception_when_any_dependency_is_not_registered()
        {
            var container = new DependencyContainer();
            container.Register<ClassWithConstructor>();

            Assert.Throws<TypeNotRegisteredException>(
                () => container.Create<ClassWithConstructor>()
            );
        }

        [Test]
        public void Should_create_instance_when_all_types_are_registerered()
        {
            var container = new DependencyContainer();
            container.Register<ClassWithConstructor>();
            container.Register<IEmptyInterface, ClassWithoutConstructor>();

            var b = container.Create<ClassWithConstructor>();

            Assert.That(b, Is.Not.Null);
            Assert.That(b.A, Is.Not.Null);
        }

        [Test]
        public void Should_set_object_when_register_interface()
        {
            var container = new DependencyContainer();
            container.RegisterAction<ISetProperty>(obj => obj.Property = "test_property");
            container.Register<ClassWithConstructor>();
            container.Register<IEmptyInterface, ClassWithoutConstructor>();

            var a = container.Create<ClassWithoutConstructor>();
            Assert.That(a.Property, Is.EqualTo("test_property"));

            var b = container.Create<ClassWithConstructor>();

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
            container.Register<DependencySetupClass>();

            var c = container.Create<DependencySetupClass>();

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

            var c = new DependencySetupClass();
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
            container.Register<MultiDependencySetup>();

            var d = container.Create<MultiDependencySetup>();

            Assert.That(d.P1, Is.EqualTo("a"));
            Assert.That(d.P2, Is.EqualTo(1));
        }

        [Test]
        public void Should_singleton_instance_when_registered()
        {
            var container = new DependencyContainer();
            container.RegisterSingleton<IEmptyInterface, ClassWithoutConstructor>();

            var obj1 = container.Create<IEmptyInterface>();
            var obj2 = container.Create<IEmptyInterface>();

            Assert.That(obj1, Is.Not.Null);
            Assert.That(obj2, Is.Not.Null);
            Assert.That(obj1, Is.EqualTo(obj2));
        }

        [Test]
        public void Should_instantiate_with_multiple_dependency_setup_recursion()
        {
            var container = new DependencyContainer();
            container.Register("a");
            container.Register(1);
            container.Register(true);
            container.Register<DependencySetupClass>();
            container.Register<MultiDependencySetup>();
            container.Register<DependencySetupRecursion>();

            var instance = container.Create<DependencySetupRecursion>();

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
            var container = new DependencyContainer();
            container.Register("test");
            container.Register<Factory>();

            var factory = container.Create<Factory>();

            Assert.That(factory.Instantiate(), Is.EqualTo("test"));
        }
    }

    public partial class DependencyContainerTests
    {
        public interface IEmptyInterface { }
        public class ClassWithoutConstructor : IEmptyInterface, ISetProperty
        {
            public string Property { get; set; }
        }

        public interface ISetProperty
        {
            public string Property { get; set; }
        }

        public class ClassWithConstructor : ISetProperty
        {
            public ClassWithConstructor(IEmptyInterface a)
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

        public class DependencySetupClass : IDependencySetup<SetupParameters>
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
        }

        public class DependencySetupRecursion
            : IDependencySetup<DependencySetupClass, MultiDependencySetup>
        {
            public DependencySetupClass Dependency { get; private set; }
            public MultiDependencySetup Dependency2 { get; private set; }

            public void Setup(DependencySetupClass p1, MultiDependencySetup p2)
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
                return Container.Create<string>();
            }
        }
    }
}
