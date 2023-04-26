using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.HealthSystem.Tests
{
    public class SimpleHealthBarViewTests
    {
        string healthBarPath = "Assets/UnityFoundation/GameplaySystems/HealthSystem/UI/HealthBar/SimpleView/simple_healthbar.prefab";

        private SimpleHealthBarView bar;

        [OneTimeSetUp]
        public void Setup()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(healthBarPath);
            bar = Object.Instantiate(prefab).GetComponent<SimpleHealthBarView>();
        }

        [OneTimeTearDown]
        public void Clean()
        {
            Object.DestroyImmediate(bar.gameObject);
        }

        [Test]
        public void Should_be_awakable_with_base_components()
        {
            Assert.DoesNotThrow(() => bar.Awake());
        }

        [Test]
        public void Given_health_is_max_should_be_full()
        {
            bar.Awake();
            bar.Setup(100f);

            Assert.AreEqual(0f, GetBarProgress().sizeDelta.x);
        }

        [Test]
        public void Should_be_minimum_when_health_is_zero()
        {
            bar.Awake();
            bar.Setup(100f);

            var baseRect = GetBaseRect();
            bar.SetCurrentHealth(0f);

            Assert.That(GetBarProgress().sizeDelta.x, Is.EqualTo(-baseRect.sizeDelta.x));
        }

        [Test]
        [TestCase(100f, 50f)]
        [TestCase(100f, 20f)]
        public void Given_current_health_should_partially_fill_health_bar(
            float baseHealth, float currentHealth
        )
        {
            bar.Awake();
            bar.Setup(baseHealth);
            bar.SetCurrentHealth(currentHealth);

            var healthRate = 1f - currentHealth / baseHealth;
            var expected = -(GetBaseRect().sizeDelta.x * healthRate);
            Assert.That(GetBarProgress().sizeDelta.x, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(200f, 100f, 0f)]
        [TestCase(200f, 100f, 50f)]
        [TestCase(300f, 100f, 20f)]
        public void Given_size_is_different_should_behaviour_the_same_way(
            float xSize, float baseHealth, float currentHealth
        )
        {
            var baseRect = GetBaseRect();
            baseRect.sizeDelta = new Vector2(xSize, baseRect.sizeDelta.y);

            bar.Awake();
            bar.Setup(baseHealth);
            bar.SetCurrentHealth(currentHealth);

            var healthRate = 1f - currentHealth / baseHealth;
            var expected = -(baseRect.sizeDelta.x * healthRate);
            Assert.That(GetBarProgress().sizeDelta.x, Is.EqualTo(expected));
        }

        private RectTransform GetBaseRect()
        {
            return bar.GetComponent<RectTransform>();
        }

        private RectTransform GetBarProgress()
        {
            return bar.transform.FindComponent<RectTransform>("bar", "bar_progress");
        }
    }
}