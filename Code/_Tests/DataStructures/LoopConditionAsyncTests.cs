using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityFoundation.TestUtility;

namespace UnityFoundation.Code.Tests
{
    public class LoopConditionAsyncTests
    {
        [Test]
        [Timeout(1000)]
        public async Task Should_not_execute_when_condition_is_initially_not_true()
        {
            var action = new ActionTestHelper();

            var alwaysFalse = Condition.Create(() => false);
            await LoopConditionAsync.While(alwaysFalse).Loop(action.Action);

            Assert.That(action.WasExecuted, Is.False);
        }

        [Test]
        [Timeout(1000)]
        public async Task Should_execute_when_condition_is_initially_true()
        {
            var action = new ActionTestHelper();

            var condition = new Mock<ICondition>();
            condition.SetupSequence(condition => condition.Resolve())
                .Returns(true)
                .Returns(false);

            await LoopConditionAsync.While(condition.Object).Loop(action.Action);

            Assert.That(action.WasExecuted, Is.True);
            Assert.That(action.TimesExecuted, Is.EqualTo(1));
        }

        [Test]
        [Timeout(1000)]
        public async Task Should_execute_while_condition_is_true()
        {
            var action = new ActionTestHelper();

            var condition = new Mock<ICondition>();
            condition.SetupSequence(condition => condition.Resolve())
                .Returns(true)
                .Returns(true)
                .Returns(true)
                .Returns(true)
                .Returns(false);

            await LoopConditionAsync.While(condition.Object).Loop(action.Action);

            Assert.That(action.WasExecuted, Is.True);
            Assert.That(action.TimesExecuted, Is.EqualTo(4));
        }
    }
}
