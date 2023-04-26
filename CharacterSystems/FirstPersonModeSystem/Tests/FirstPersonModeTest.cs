using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.FirstPersonModeSystem.Tests
{
    public class FirstPersonModeTest
    {
        private void BuildFirstPersonModePlayer(
            out FirstPersonMode firstPerson,
            out Mock<ICheckGroundHandler> checkGroundMock,
            out Mock<IRigidbody> rigidBodyMock)
        {
            firstPerson = new GameObject("player").AddComponent<FirstPersonMode>();

            checkGroundMock = new Mock<ICheckGroundHandler>();
            var inputActionsMock = new Mock<FirstPersonInputActions>();

            var inputsMock = new Mock<IFirstPersonInputs>();
            inputsMock.SetupGet(x => x.Jump).Returns(true);

            firstPerson.Setup(
                ScriptableObject.CreateInstance<FirstPersonModeSettings>(),
                inputsMock.Object,
                checkGroundMock.Object,
                animController: null,
                audioSource: new Mock<IAudioSource>().Object,
                camera: null
            );

            rigidBodyMock = new Mock<IRigidbody>();
            firstPerson.Rigidbody = rigidBodyMock.Object;
        }

        [Test]
        public void ShouldNotJumpWhenWasNotOnGround()
        {
            BuildFirstPersonModePlayer(
                out FirstPersonMode firstPerson,
                out Mock<ICheckGroundHandler> checkGroundMock,
                out Mock<IRigidbody> rigidBodyMock
            );

            checkGroundMock.SetupGet(cg => cg.IsGrounded).Returns(false);

            firstPerson.TryJump();

            rigidBodyMock.Verify(
                rb => rb.AddForce(It.IsAny<Vector3>(), It.IsAny<ForceMode>()),
                Times.Never()
            );
        }

        [Test]
        public void ShouldJumpWhenWasOnGround()
        {
            BuildFirstPersonModePlayer(
                out FirstPersonMode firstPerson,
                out Mock<ICheckGroundHandler> checkGroundMock,
                out Mock<IRigidbody> rigidBodyMock
            );

            checkGroundMock.SetupGet(cg => cg.IsGrounded).Returns(true);

            firstPerson.TryJump();

            checkGroundMock.SetupGet(cg => cg.IsGrounded).Returns(false);

            firstPerson.TryJump();
            firstPerson.TryJump();

            rigidBodyMock.Verify(
                rb => rb.AddForce(It.IsAny<Vector3>(), It.IsAny<ForceMode>()),
                Times.Once()
            );
        }

        [Test]
        public void ShouldNotFailIfOnShotHitHasNoSubscribes()
        {
            BuildFirstPersonModePlayer(
                out FirstPersonMode firstPerson,
                out _,
                out _
            );

            var randomPoint = Vector3Utils.RandomPoint();

            Assert.DoesNotThrow(() => firstPerson.ShootHit(randomPoint));
        }

        [Test]
        public void OnShootHitBehaviour()
        {
            BuildFirstPersonModePlayer(
                out FirstPersonMode firstPerson,
                out _,
                out _
            );

            var randomPoint = Vector3Utils.RandomPoint();

            void expected(Vector3 point) => Assert.AreEqual(randomPoint, point);
            firstPerson.OnShotHit += expected;

            firstPerson.ShootHit(randomPoint);
        }
    }
}