using NUnit.Framework;
using UnityFoundation.Code;

namespace UnityFoundation.AmmoStorageSystem.Tests
{
    public class AmmoStorageTest
    {
        [Test]
        [TestCase(5u)]
        [TestCase(10u)]
        [TestCase(15u)]
        public void ShouldNotRecoverMoreThanMaxAmmunition(uint recoveryAmount)
        {
            var maxAmmuntion = 10u;
            var storage = new AmmoStorage(maxAmmuntion);

            storage.Recover(recoveryAmount);

            Assert.AreEqual(recoveryAmount.Clamp(0, storage.MaxAmount), storage.CurrentAmount);
        }

        [Test]
        [TestCase(0u, 0u)]
        [TestCase(4u, 4u)]
        [TestCase(15u, 10u)]
        public void ShouldGetXAmmoWhenRequested(uint requestAmmo, uint expected)
        {
            var maxAmmuntion = 10u;
            var storage = new AmmoStorage(maxAmmuntion);

            storage.FullReffil();

            var ammo = storage.GetAmmo(requestAmmo);

            Assert.AreEqual(expected, ammo);
        }

        [Test]
        [TestCase(4u)]
        public void ShouldBeEmptyWhenRequestAllStorageAmmo(uint requestAmmo)
        {
            var storage = new AmmoStorage(requestAmmo);

            storage.FullReffil();

            var ammo = storage.GetAmmo(requestAmmo);

            Assert.AreEqual(requestAmmo, ammo);
            Assert.IsTrue(storage.Empty);
        }
    }
}