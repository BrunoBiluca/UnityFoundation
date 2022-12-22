using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityFoundation.TestUtility;

namespace UnityFoundation.Code.UnityAdapter.Tests
{
    public class TransformNavegationAgentTests
    {
        [Test]
        public void Should_not_move_if_there_is_no_destination_setup()
        {
            var transform = BuildTransformMock();
            var moveHandler = new TransformNavegationAgent(transform.Object);

            moveHandler.Update();

            AssertHelper.AreEqual(moveHandler.CurrentPosition, Vector3.zero);
        }

        [Test]
        public void Given_a_destination_should_move_after_update()
        {
            var transform = BuildTransformMock();

            var moveHandler = new TransformNavegationAgent(transform.Object);
            moveHandler.Speed = 1;

            moveHandler.SetDestination(new Vector3(2, 0, 0));

            AssertHelper.AreEqual(moveHandler.CurrentPosition, Vector3.zero);
            Assert.AreEqual(2, moveHandler.RemainingDistance);

            moveHandler.Update();

            Assert.AreEqual(1, moveHandler.RemainingDistance);
            AssertHelper.AreNotEqual(moveHandler.CurrentPosition, Vector3.zero);
        }

        [Test]
        public void Given_a_destination_should_move_until_reaches_stopping_distance()
        {
            var transform = BuildTransformMock();

            var moveHandler = new TransformNavegationAgent(transform.Object) {
                StoppingDistance = 0.2f
            };

            moveHandler.SetDestination(new Vector3(2.1f, 0, 0));

            moveHandler.Update();
            moveHandler.Update();

            Assert.AreEqual(0f, moveHandler.RemainingDistance);
            AssertHelper.AreEqual(moveHandler.CurrentPosition, new Vector3(2.0f, 0, 0));
        }


        [Test]
        public void Given_a_destination_should_move_according_to_the_amount_rate()
        {
            var transform = BuildTransformMock();

            var moveHandler = new TransformNavegationAgent(transform.Object);

            var destination = new Vector3(2f, 0, 0);
            moveHandler.SetDestination(destination);

            moveHandler.Update(0.5f);
            moveHandler.Update(0.5f);

            Assert.AreEqual(1f, moveHandler.RemainingDistance);

            moveHandler.Update(0.5f);
            moveHandler.Update(0.5f);

            Assert.AreEqual(0f, moveHandler.RemainingDistance);
            AssertHelper.AreEqual(moveHandler.CurrentPosition, destination);
        }

        [Test]
        public void Given_a_destination_should_move_until_reaches_it()
        {
            var transform = BuildTransformMock();
            var transformNavegation = new TransformNavegationAgent(transform.Object);

            var haveReachedDestination = false;
            transformNavegation.OnReachDestination += () => haveReachedDestination = true;

            var destination = new Vector3(2, 0, 0);
            transformNavegation.SetDestination(destination);
            transformNavegation.Speed = 1f;

            AssertHelper.AreEqual(transformNavegation.CurrentPosition, Vector3.zero);

            transformNavegation.Update();

            AssertHelper.AreNotEqual(transformNavegation.CurrentPosition, destination);

            transformNavegation.Update();

            Assert.IsTrue(haveReachedDestination);
            AssertHelper.AreEqual(transformNavegation.CurrentPosition, destination);

            transformNavegation.Update();
            transformNavegation.Update();

            AssertHelper.AreEqual(transformNavegation.CurrentPosition, destination);
        }

        private Mock<ITransform> BuildTransformMock()
        {
            var transform = new Mock<ITransform>();
            var position = Vector3.zero;
            transform
                .SetupSet((t) => {
                    t.Position = It.IsAny<Vector3>();
                })
                .Callback<Vector3>((pos) => {
                    position = pos;
                });
            transform
                .SetupGet((t) => t.Position)
                .Returns(() => position);
            return transform;
        }
    }
}