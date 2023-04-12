using NUnit.Framework;
using UnityEngine;

namespace UnityFoundation.Code.Tests
{
    public class DependencySetupAdapterTests
    {
        public class MonoWithoutSetupClass : MonoBehaviour { }

        public class MonoWithSetupClass : MonoBehaviour
        {
            public string Name { get; private set; }
            public void Setup(string name) { Name = name; }
        }

        [Test]
        public void Should_throw_error_when_try_to_register_class_has_no_setup_method()
        {
            var obj = new GameObject("test_setup_adapter").AddComponent<MonoWithoutSetupClass>();

            var binder = new DependencyBinder();
            binder.Register("name mono class");
            binder.RegisterSetup(obj);

            Assert.Throws<TypeWithNoSetupMethodException>(
                () => binder.Build().Resolve<MonoWithoutSetupClass>()
            );
        }

        [Test]
        public void Should_bind_class_with_setup_adapter()
        {
            var obj = new GameObject("test_setup_adapter").AddComponent<MonoWithSetupClass>();

            var binder = new DependencyBinder();
            binder.Register("name mono class");
            binder.RegisterSetup(obj);

            binder.Build().Resolve<MonoWithSetupClass>();

            Assert.That(obj.Name, Is.EqualTo("name mono class"));
        }
    }
}
