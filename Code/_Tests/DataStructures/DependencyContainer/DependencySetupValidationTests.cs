using NUnit.Framework;
using System.Linq;

namespace UnityFoundation.Code.Tests
{
    public class DependencySetupValidationTests
    {
        public class NoDependencySetup { }

        public class SingleDependencySetup : IDependencySetup<int>
        {
            public void Setup(int parameters) { }
        }

        public class MultiDependencySetup : IDependencySetup<int>, IDependencySetup<int, bool>
        {
            public void Setup(int parameters) { }

            public void Setup(int p1, bool p2) { }
        }

        [Test]
        public void Should_return_no_method_when_class_dont_implement_IDependencySetup()
        {
            var methods = DependencySetupValidation.GetMethods(typeof(NoDependencySetup));

            Assert.That(methods, Is.Empty);
        }

        [Test]
        public void Should_return_one_method_when_class_implement_IDependencySetup()
        {
            var methods = DependencySetupValidation.GetMethods(typeof(SingleDependencySetup));

            Assert.That(methods.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Should_return_multiple_method_when_class_implement_IDependencySetup()
        {
            var methods = DependencySetupValidation.GetMethods(typeof(MultiDependencySetup));

            Assert.That(methods.Count(), Is.EqualTo(2));
        }
    }
}
