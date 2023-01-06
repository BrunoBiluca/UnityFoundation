using Moq;
using NUnit.Framework;
using System;
using UnityFoundation.CharacterSystem.ActorSystem;

namespace UnityFoundation.CharacterSystem.ActorSystem.Tests
{
    public partial class UnitActionsManagerTests
    {

        ContextBuilder context;

        [SetUp]
        public void Setup()
        {
            context = new ContextBuilder();
        }

        [Test]
        public void Should_not_execute_action_if_no_actions_are_setup()
        {
            var actionsManager = context.Build();

            actionsManager.Execute();

            context.MockIntent.Verify((a) => a.Create(), Times.Never());
        }

        [Test]
        public void Should_not_execute_action_if_action_was_unset()
        {
            var actionManager = context.WithActionSet().Build();

            actionManager.UnsetAction();

            actionManager.Execute();

            context.MockIntent.Verify((a) => a.Create(), Times.Never());
        }

        [Test]
        public void Should_execute_immediatly_when_action_immediate_and()
        {
            var actionManager = context
                .WithImmediateAction()
                .WithInitialActionPoints(1)
                .Build();

            context.MockIntent.Verify((a) => a.Create(), Times.Once());
        }

        [Test]
        public void Should_not_execute_immediatly_when_has_no_action_points()
        {
            var actionManager = context.WithInitialActionPoints(0).Build();

            var mockAction = new Mock<IAPActionIntent>();
            mockAction.SetupGet(a => a.ActionPointsCost).Returns(2);
            mockAction.SetupGet(a => a.ExecuteImmediatly).Returns(true);
            actionManager.Set(mockAction.Object);

            context.MockIntent.Verify((a) => a.Create(), Times.Never());
        }

        [Test]
        [TestCase(0, 1, TestName = "never when has zero action points")]
        [TestCase(1, 1, TestName = "one when has one action points")]
        [TestCase(3, 3, TestName = "three when has three action points")]
        [TestCase(3, 5, TestName = "3 times of 5 when has only three action points")]
        public void Should_execute_action(int points, int executions)
        {
            var actor = context
                .WithActionSet()
                .WithInitialActionPoints(points)
                .Build();

            for(int i = 0; i < executions; i++)
                actor.Execute();

            Assert.That(context.CantExecuteAction.WasTriggered, Is.EqualTo(executions > points));
            Assert.That(
                context.CantExecuteAction.TriggerCount,
                Is.EqualTo(Math.Abs(points - executions))
            );

            Assert.That(actor.Intent.IsPresent, Is.True);

            var remainingPoints = Math.Clamp(points - executions, 0, points);
            Assert.That(actor.ActionPoints.CurrentAmount, Is.EqualTo(remainingPoints));

            context.MockIntent.Verify((a) => a.Create(), Times.Exactly(points));
        }

        [Test]
        public void Should_not_execute_action_if_action_point_cost_is_higher()
        {
            var actionManager = context
                .WithActionSet(3)
                .WithInitialActionPoints(2)
                .Build();

            actionManager.Execute();

            context.MockIntent.Verify((a) => a.Create(), Times.Never());
        }

        [Test]
        public void Should_execute_action_with_more_action_points()
        {
            var actionManager = context
                .WithActionSet(3)
                .WithInitialActionPoints(4)
                .Build();

            actionManager.Execute();

            context.MockIntent.Verify((a) => a.Create(), Times.Once());
            Assert.That(actionManager.ActionPoints.CurrentAmount, Is.EqualTo(1));
        }
    }
}