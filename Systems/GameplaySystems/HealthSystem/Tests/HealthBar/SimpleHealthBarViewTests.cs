using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.HealthSystem.Tests
{
    public class SimpleHealthBarViewTests
    {
        string healthBarPath = "Assets/UnityFoundation/Systems/GameplaySystems/HealthSystem/HealthBar/SimpleView/simple_healthbar.prefab";

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
        public void ShouldBeAwakeableWithBaseComponents()
        {
            Assert.DoesNotThrow(() => bar.Awake());
        }

        [Test]
        public void ShouldBeMinimumWhenHealthIsZero()
        {
            bar.Awake();
            bar.Setup(100f);

            var baseRect = GetBaseRect();
            var originalBaseRect = baseRect.sizeDelta.x;

            var barProgress = GetBarProgress();

            bar.SetCurrentHealth(0f);

            Assert.AreEqual(originalBaseRect, baseRect.sizeDelta.x);
            Assert.AreEqual(0f, barProgress.sizeDelta.x);
        }

        [Test]
        public void ShouldBeFullWhenHealthIsOnMax()
        {
            bar.Awake();
            bar.Setup(100f);

            AssertCurrentRate(100f, 1f);
        }

        [Test]
        [TestCase(100f, 50f, 0.5f)]
        [TestCase(100f, 20f, 0.2f)]
        public void ShouldBeXWhenCurrentHealthPartiallyBaseHealth(
            float baseHealth, float currentHealth, float expectedRate
        )
        {
            bar.Awake();
            bar.Setup(baseHealth);

            AssertCurrentRate(currentHealth, expectedRate);
        }

        [Test]
        [TestCase(200f, 100f, 0f, 0f)]
        [TestCase(200f, 100f, 50f, 0.5f)]
        [TestCase(200f, 100f, 20f, 0.2f)]
        public void ShouldBehaviourSameWayWithDifferentBaseRectSize(
            float xSize, float baseHealth, float currentHealth, float expectedRate
        )
        {
            var baseRect = GetBaseRect();
            baseRect.sizeDelta = new Vector2(xSize, baseRect.sizeDelta.y);

            bar.Awake();
            bar.Setup(baseHealth);

            AssertCurrentRate(currentHealth, expectedRate);
        }

        private void AssertCurrentRate(float currentHealth, float expectedRate)
        {
            var baseRect = GetBaseRect();
            var barProgress = GetBarProgress();

            Assert.AreEqual(baseRect.sizeDelta.x, barProgress.sizeDelta.x);

            bar.SetCurrentHealth(currentHealth);

            Assert.AreEqual(expectedRate, barProgress.sizeDelta.x / baseRect.sizeDelta.x);
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