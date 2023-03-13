using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Moq;
using UnityFoundation.Code.Timer;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Physics3D.CheckGround
{
    [TestFixture(typeof(CheckGroundHandlerTestFixture))]
    [TestFixture(typeof(DebugGroundCheckerTestFixture))]
    public class CheckGroundHandlerTest<T> where T : BaseCheckGroundTestFixture, new()
    {
        private T testFixture;

        [SetUp]
        public void SetUp()
        {
            testFixture = new T();
        }

        [Test]
        [TestCaseSource(nameof(CollisionCases))]
        public void Should_be_grounded_when_collide_with_floor(
            float objPos, float groundedOff, bool expected
        )
        {
            var floor = new FloorBuilder().Build();
            var groundedObject = new GroundedObjectBuilder().WithPosition(objPos).Build();

            testFixture.Collider = groundedObject.GetComponent<CapsuleCollider>().Decorate();
            testFixture.GroundedOffset = groundedOff;

            var checkGroundHandler = testFixture.Build();
            checkGroundHandler.CheckGround();

            Assert.AreEqual(expected, checkGroundHandler.IsGrounded);

            Object.DestroyImmediate(floor);
            Object.DestroyImmediate(groundedObject);
        }

        [Test]
        public void Should_disabled_check_for_X_time()
        {
            var floor = new FloorBuilder().Build();
            var groundedObject = new GroundedObjectBuilder().Build();

            testFixture.Collider = groundedObject.GetComponent<CapsuleCollider>().Decorate();
            testFixture.GroundedOffset = 0.1f;

            var checkGroundHandler = testFixture.Build();
            checkGroundHandler.CheckGround();

            Assert.True(checkGroundHandler.IsGrounded);

            var timerMock = new Mock<ITimer>();
            checkGroundHandler.Disable(timerMock.Object);

            checkGroundHandler.CheckGround();
            Assert.False(checkGroundHandler.IsGrounded);

            timerMock.SetupGet(t => t.Completed).Returns(true);
            Assert.False(checkGroundHandler.IsGrounded);

            checkGroundHandler.CheckGround();
            Assert.True(checkGroundHandler.IsGrounded);

            Object.DestroyImmediate(floor);
            Object.DestroyImmediate(groundedObject);
        }

        private static IEnumerable<TestCaseData> CollisionCases()
        {
            yield return new TestCaseData(1.51f, 0.0f, false)
                .SetName("Object close to the floor with small offset");
            yield return new TestCaseData(1.5f, 0.1f, true)
                .SetName("Object close to the floor with good offset");
            yield return new TestCaseData(1.61f, 0.1f, false)
                .SetName("Floor far away with small offset");
            yield return new TestCaseData(1.6f, 0.2f, true)
                .SetName("Floor far away with bigger offset");
        }

    }
}