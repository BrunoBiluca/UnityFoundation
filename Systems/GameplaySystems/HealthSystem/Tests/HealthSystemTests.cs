using NUnit.Framework;
using UnityEngine;
using UnityFoundation.TestUtility;

namespace UnityFoundation.HealthSystem.Tests
{
    [TestFixture(typeof(HealthSystemFactory), TestName = "Testing HealthSystem")]
    [TestFixture(typeof(HealthSystemMonoFactory), TestName = "Testing HealthSystemMono")]
    public class HealthSystemTests<T> where T : IHealthSystemFactory, new()
    {
        private IHealthSystem healthSystem;

        [SetUp]
        public void Setup()
        {
            healthSystem = new T().Create();
        }

        [Test]
        public void Should_be_with_full_health_when_set_to_setup_on_start()
        {
            healthSystem.Setup(10);

            Assert.That(healthSystem.BaseHealth, Is.EqualTo(10));
            Assert.That(healthSystem.CurrentHealth, Is.EqualTo(10));
            Assert.That(healthSystem.IsDead, Is.False);
        }

        [Test]
        [TestCase(
            10f, 5f,
            TestName = "be the difference when damage is smaller than base health"
        )]
        [TestCase(
            10f, 15f,
            TestName = "be zero when damage is bigger than base health"
        )]
        public void When_damaged_health_system_should(float baseHealth, float damage)
        {
            healthSystem.Setup(baseHealth);

            var onTakeDamage = EventTest.Create(healthSystem, nameof(healthSystem.OnTakeDamage));
            var onDied = EventTest.Create(healthSystem, nameof(healthSystem.OnDied));

            healthSystem.Damage(damage);

            var expectedHealth = damage >= baseHealth
            ? 0f
            : baseHealth - damage;

            Assert.That(healthSystem.BaseHealth, Is.EqualTo(baseHealth));
            Assert.That(healthSystem.CurrentHealth, Is.EqualTo(expectedHealth));

            Assert.That(onDied.WasTriggered, Is.EqualTo(healthSystem.IsDead));
            Assert.That(onTakeDamage.WasTriggered, Is.Not.EqualTo(healthSystem.IsDead));
        }

        [Test]
        [TestCase(10f, 1f, 7f, TestName = "health amount when is smaller than damage")]
        [TestCase(10f, 1f, 12f, TestName = "health amount fully when is equal or bigger")]
        public void When_healed_health_system_should(
            float baseHealth, float initialHealth, float healAmount
        )
        {
            healthSystem.Setup(baseHealth);

            var damage = baseHealth - initialHealth;
            healthSystem.Damage(damage);

            healthSystem.Heal(healAmount);

            var expectedHealth = healAmount >= damage
                ? healthSystem.BaseHealth
                : initialHealth + healAmount;

            Assert.That(healthSystem.CurrentHealth, Is.EqualTo(expectedHealth));
        }

        [Test]
        public void Should_not_damage_when_is_already_dead()
        {
            healthSystem.Setup(10f);

            var onTakeDamage = EventTest.Create(healthSystem, nameof(healthSystem.OnTakeDamage));

            healthSystem.Damage(5f);
            healthSystem.Damage(5f);
            healthSystem.Damage(5f);

            Assert.That(onTakeDamage.WasTriggered, Is.True);
            Assert.That(onTakeDamage.TriggerCount, Is.EqualTo(1));
        }
    }
}