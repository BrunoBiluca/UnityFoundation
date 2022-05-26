using Assets.UnityFoundation.UnityAdapter;
using Moq;
using NUnit.Framework;

namespace UnityFoundation.FirstPersonModeSystem.Tests
{
    public class FirstPlayerAnimationControllerTest
    {
        [Test]
        public void ShouldPlayFire_WhenCalledFireAndAnimatorInAim()
        {
            var animMock = new Mock<IAnimator>();
            var firstPlayerAnimController = new FirstPersonAnimationController(animMock.Object);
            firstPlayerAnimController.Fire();
            animMock.Verify(m => m.SetTrigger(FirstPersonAnimationParams.FIRE), Times.Once());
        }

        [Test]
        public void ShouldPlayAim_WhenCalledAim()
        {
            var animMock = new Mock<IAnimator>();
            animMock.SetupSequence(m => m.GetBool(FirstPersonAnimationParams.AIM))
                .Returns(false)
                .Returns(true)
                .Returns(false);

            var firstPlayerAnimController = new FirstPersonAnimationController(animMock.Object);

            firstPlayerAnimController.ToggleAim();
            animMock.Verify(m => m.SetBool(FirstPersonAnimationParams.AIM, true));

            firstPlayerAnimController.ToggleAim();
            animMock.Verify(m => m.SetBool(FirstPersonAnimationParams.AIM, false));

            firstPlayerAnimController.ToggleAim();
            animMock.Verify(m => m.SetBool(FirstPersonAnimationParams.AIM, true));
        }

        [Test]
        public void ShouldPlayReload_WhenCalledReload()
        {
            var animMock = new Mock<IAnimator>();
            var firstPlayerAnimController = new FirstPersonAnimationController(animMock.Object);
            firstPlayerAnimController.Reload();
            animMock.Verify(m => m.SetTrigger(FirstPersonAnimationParams.RELOAD), Times.Once());
        }

        [Test]
        public void ShouldPlayWalking_WhenCalledWalking()
        {
            var animMock = new Mock<IAnimator>();
            var firstPlayerAnimController = new FirstPersonAnimationController(animMock.Object);

            firstPlayerAnimController.Walking(true);
            animMock.Verify(
                m => m.SetBool(FirstPersonAnimationParams.WALKING, true), Times.Once()
            );

            firstPlayerAnimController.Walking(false);
            animMock.Verify(
                m => m.SetBool(FirstPersonAnimationParams.WALKING, false), Times.Once()
            );
        }
    }
}