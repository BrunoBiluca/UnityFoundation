using NUnit.Framework;
using System;

namespace UnityFoundation.Code.Tests
{
    public class EvaluationTests
    {
        [Test]
        public void Should_throw_error_when_no_callback_was_setup()
        {
            var evaluation = new ValueEvaluation<int>();

            Assert.Throws<NullReferenceException>(() => evaluation.Eval());
        }

        [Test]
        public void Should_not_do_action_when_a_single_expression_is_not_attended()
        {
            static int callback() => 10;
            var evaluation = new ValueEvaluation<int>(callback);

            var wasTriggered = false;
            evaluation.If(val => val == 10).Do(() => wasTriggered = true);

            evaluation.Eval();

            Assert.That(wasTriggered, Is.True);
        }

        [Test]
        public void Should_do_action_when_a_single_expression_is_attended()
        {
            static int callback() => 0;
            var evaluation = new ValueEvaluation<int>(callback);

            var wasTriggered = false;
            evaluation.If(val => val == 10).Do(() => wasTriggered = true);

            evaluation.Eval();

            Assert.That(wasTriggered, Is.False);
        }

        [Test]
        public void Should_execute_action_of_the_first_condition_attended()
        {
            static int callback() => 10;
            var evaluation = new ValueEvaluation<int>(callback);

            var wasTriggered1 = false;
            var wasTriggered2 = false;
            evaluation.If(val => val == 10).Do(() => wasTriggered1 = true);
            evaluation.If(val => val == 10).Do(() => wasTriggered2 = true);

            evaluation.Eval();

            Assert.That(wasTriggered1, Is.True);
            Assert.That(wasTriggered2, Is.False);
        }
    }
}
