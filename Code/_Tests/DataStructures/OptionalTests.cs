using NUnit.Framework;
using System;

namespace UnityFoundation.Code.Tests
{
    public class OptionalTests
    {
        [Test]
        public void Should_change_value_when_value_is_present()
        {
            var optional = new Optional<int>(3);
            Assert.That(optional.Some(v => v + 1).Get(), Is.EqualTo(4));
        }

        [Test]
        public void Should_not_execute_function_when_value_is_not_present()
        {
            var optional = Optional<int>.None();

            var wasCalled = false;
            int func(int v)
            {
                wasCalled = true;
                return v;
            }

            optional.Some(func);

            Assert.That(wasCalled, Is.False);
        }
    }
}
