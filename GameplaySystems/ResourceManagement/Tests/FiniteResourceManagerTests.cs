using NUnit.Framework;
using UnityFoundation.Code;

namespace UnityFoundation.ResourceManagement.Tests
{
    public class FiniteResourceManagerTests
    {
        [Test]
        [TestCase(5u)]
        [TestCase(10u)]
        [TestCase(15u)]
        public void Should_not_recover_more_than_max_amout(uint recoveryAmount)
        {
            var maxAmmuntion = 10u;
            var storage = new FiniteResourceManager(maxAmmuntion);

            storage.Add(recoveryAmount);

            Assert.AreEqual(recoveryAmount.Clamp(0, storage.MaxAmount), storage.CurrentAmount);
        }

        [Test]
        [TestCase(0u, 0u)]
        [TestCase(4u, 4u)]
        [TestCase(15u, 10u)]
        public void Should_get_x_amount_when_request(uint requestAmmo, uint expected)
        {
            var maxAmmuntion = 10u;
            var storage = new FiniteResourceManager(maxAmmuntion);

            storage.FullReffil();

            var ammo = storage.GetAmount(requestAmmo);

            Assert.AreEqual(expected, ammo);
        }

        [Test]
        [TestCase(4u)]
        public void Should_be_empty_when_request_all(uint requestAmmo)
        {
            var storage = new FiniteResourceManager(requestAmmo);

            storage.FullReffil();

            var ammo = storage.GetAmount(requestAmmo);

            Assert.AreEqual(requestAmmo, ammo);
            Assert.IsTrue(storage.IsEmpty);
        }

        [Test]
        [TestCase(4u, 10u, 6u, true)]
        [TestCase(10u, 10u, 0u, true)]
        [TestCase(15u, 10u, 10u, false)]
        public void Given_an_amount_should_return_if_has_amount_to_subtract(
            uint subtractAmount, uint initialAmount, uint expectedEndAmount, bool expected
        )
        {
            var resourceManager = new FiniteResourceManager(initialAmount, startFull: true);

            var hasAmount = resourceManager.TrySubtract(subtractAmount);

            Assert.That(hasAmount, Is.EqualTo(expected));
            Assert.That(resourceManager.CurrentAmount, Is.EqualTo(expectedEndAmount));
        }
    }
}