using NUnit.Framework;

namespace UnityFoundation.Code.Tests
{
    public class DependencyBinderTests
    {
        public class Parameters
        {

            public Parameters() { }

            public Parameters(string a, int b, bool c)
            {
                A = a;
                B = b;
                C = c;
            }

            public string A { get; }
            public int B { get; }
            public bool C { get; }
        }

        public class DependencySetup : IDependencySetup<Parameters>
        {
            public string P1 { get; private set; }
            public int P2 { get; private set; }
            public bool P3 { get; private set; }

            public void Setup(Parameters p1)
            {
                P1 = p1.A;
                P2 = p1.B;
                P3 = p1.C;
            }
        }

        public class DependencySetupCounter : IDependencySetup<Parameters>
        {
            public int Counter { get; private set; } = 0;

            public void Setup(Parameters p1)
            {
                Counter++;
            }
        }

        [Test]
        public void Should_resolve_last_registered_constant_instance()
        {
            var binder = new DependencyBinder();
            binder.Register<DependencySetup>();

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
    }
}
