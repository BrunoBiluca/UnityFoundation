using NUnit.Framework;

namespace UnityFoundation.Code.Tests
{
    public class DependencyBinderTests
    {
        [Test]
        public void Should_resolve_last_registered_constant_instance()
        {
            var binder = new DependencyBinder();
            binder.Register<DependencySetup>();
            binder.Register(new Parameters());

            var instance = binder.Build().Resolve<DependencySetup>();

            Assert.That(instance.P1, Is.Null);
            Assert.That(instance.P2, Is.EqualTo(0));
            Assert.That(instance.P3, Is.False);

            binder.Register(new Parameters("test", 123, true));

            var instanceWithConstant = binder.Build().Resolve<DependencySetup>();

            Assert.That(instanceWithConstant.P1, Is.EqualTo("test"));
            Assert.That(instanceWithConstant.P2, Is.EqualTo(123));
            Assert.That(instanceWithConstant.P3, Is.True);
        }

        [Test]
        public void Should_resolve_dependency_setup_only_once()
        {
            var binder = new DependencyBinder();
            binder.Register<DependencySetupCounter>();
            var container = binder.Build();

            container.Resolve<DependencySetupCounter>();
            container.Resolve<DependencySetupCounter>();
            var instance = container.Resolve<DependencySetupCounter>();

            Assert.That(instance.Counter, Is.EqualTo(1));
        }

        [Test]
        public void Should_resolve_dependency_setup_only_once_when_constant()
        {
            var binder = new DependencyBinder();
            binder.Register(new DependencySetupCounter());
            var container = binder.Build();

            container.Resolve<DependencySetupCounter>();
            container.Resolve<DependencySetupCounter>();
            var instance = container.Resolve<DependencySetupCounter>();

            Assert.That(instance.Counter, Is.EqualTo(1));
        }

        [Test]
        public void Should_throw_exception_when_trying_to_resolve_instance_without_all_parameters()
        {
            var binder = new DependencyBinder();
            binder.Register(123);
            binder.Register<ResolveWithParameters>();
            var container = binder.Build();

            Assert.Throws<ParameterNotRegisteredException>(
                () => container.Resolve<ResolveWithParameters>("param_test")
            );
        }

        [Test]
        public void Should_resolve_instance_with_parameters()
        {
            var binder = new DependencyBinder();
            binder.Register(123);
            binder.Register<ResolveWithParameters>();
            var container = binder.Build();

            var instance = container.Resolve<ResolveWithParameters>("param_test", true);

            Assert.That(instance.A, Is.EqualTo("param_test"));
            Assert.That(instance.B, Is.EqualTo(123));
            Assert.That(instance.C, Is.EqualTo(true));
        }

        [Test]
        public void Should_resolve_instance_with_their_respective_parameters()
        {
            var binder = new DependencyBinder();
            binder.Register(123);
            binder.Register<ResolveWithParameters>();
            var container = binder.Build();

            var instance = container.Resolve<ResolveWithParameters>("param_test", true);

            Assert.That(instance.A, Is.EqualTo("param_test"));
            Assert.That(instance.B, Is.EqualTo(123));
            Assert.That(instance.C, Is.EqualTo(true));

            var instance2 = container.Resolve<ResolveWithParameters>("param_2", 456, false);
            Assert.That(instance2.A, Is.EqualTo("param_2"));
            Assert.That(instance2.B, Is.EqualTo(456));
            Assert.That(instance2.C, Is.EqualTo(false));
        }

        [Test]
        public void Should_resolve_instance_with_any_type_of_interface_implementation()
        {
            var binder = new DependencyBinder();
            binder.Register<ResolveWithParametersImplementation>();
            var container = binder.Build();

            var impl = container.Resolve<ResolveWithParametersImplementation>(new Key1());
            Assert.That(impl.KeyImplementation, Is.TypeOf<Key1>());

            var impl2 = container.Resolve<ResolveWithParametersImplementation>(new Key2());
            Assert.That(impl2.KeyImplementation, Is.TypeOf<Key2>());
        }

        [Test]
        public void Should_resolve_instance_with_key()
        {
            var binder = new DependencyBinder();
            binder.Register<IKeyInterface, Key1>(KeyInterfaces.KEY_1);
            binder.Register<IKeyInterface, Key2>(KeyInterfaces.KEY_2);
            var container = binder.Build();

            Assert.That(container.Resolve<IKeyInterface>(KeyInterfaces.KEY_1), Is.TypeOf<Key1>());
            Assert.That(container.Resolve<IKeyInterface>(KeyInterfaces.KEY_2), Is.TypeOf<Key2>());
        }

        [Test]
        public void Should_resolve_instance_with_key_and_instance()
        {
            var binder = new DependencyBinder();
            binder.Register<IKeyInterface>(new Key1(), KeyInterfaces.KEY_1);
            binder.Register<IKeyInterface>(new Key2(), KeyInterfaces.KEY_2);
            var container = binder.Build();

            Assert.That(container.Resolve<IKeyInterface>(KeyInterfaces.KEY_1), Is.TypeOf<Key1>());
            Assert.That(container.Resolve<IKeyInterface>(KeyInterfaces.KEY_2), Is.TypeOf<Key2>());
        }

        [Test]
        public void Should_resolve_constant_for_interface_with_container_provided()
        {
            var binder = new DependencyBinder();
            binder.Register<IKeyInterface>(new ConstantWithContainer());
            var container = binder.Build();

            var instance = (ConstantWithContainer)container.Resolve<IKeyInterface>();
            Assert.That(instance, Is.Not.Null);
            Assert.That(instance.Container, Is.Not.Null);
        }

        [Test]
        public void Should_resolve_instance_with_parameters_order()
        {
            var binder = new DependencyBinder();
            binder.Register<SameTypeParametersConstructor>();
            var container = binder.Build();

            var instance = container.Resolve<SameTypeParametersConstructor>("a", "b", "c");

            Assert.That(instance.A, Is.EqualTo("a"));
            Assert.That(instance.B, Is.EqualTo("b"));
            Assert.That(instance.C, Is.EqualTo("c"));
        }

        public enum KeyInterfaces { KEY_1, KEY_2 }
        public interface IKeyInterface { }
        public class Key1 : IKeyInterface { }
        public class Key2 : IKeyInterface { }

        public class Parameters
        {
            public string A { get; }
            public int B { get; }
            public bool C { get; }
            public Parameters() { }
            public Parameters(string a, int b, bool c) { A = a; B = b; C = c; }
        }

        public class DependencySetup : IDependencySetup<Parameters>
        {
            public string P1 { get; private set; }
            public int P2 { get; private set; }
            public bool P3 { get; private set; }

            public void Setup(Parameters p) { P1 = p.A; P2 = p.B; P3 = p.C; }
        }

        public class DependencySetupCounter : IDependencySetup<Parameters>
        {
            public int Counter { get; private set; } = 0;
            public void Setup(Parameters p1) { Counter++; }
        }

        public class ResolveWithParameters
        {
            public string A { get; }
            public int B { get; }
            public bool C { get; }
            public ResolveWithParameters(string a, int b, bool c) { A = a; B = b; C = c; }
        }

        public class ResolveWithParametersImplementation
        {
            public ResolveWithParametersImplementation(IKeyInterface keyImplementation)
            {
                KeyImplementation = keyImplementation;
            }

            public IKeyInterface KeyImplementation { get; }
        }

        public class ConstantWithContainer : IKeyInterface, IContainerProvide
        {
            public IDependencyContainer Container { get; set; }
        }

        public class SameTypeParametersConstructor
        {
            public SameTypeParametersConstructor(string a, string b, string c)
            {
                A = a;
                B = b;
                C = c;
            }

            public string A { get; }
            public string B { get; }
            public string C { get; }
        }
    }
}
