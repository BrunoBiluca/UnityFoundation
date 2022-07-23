using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnityFoundation.ThirdPersonCharacter.Tests
{
    public class ProjectileTest
    {
        [UnityTest]
        [RequiresPlayMode]
        [TestCaseSource(nameof(ShouldNotHitTestCaseSource))]
        public IEnumerator ShouldNotHitBeforeHitCollider(
            float speed, float colliderPos, float waitForSeconds
        )
        {
            var projectile = new GameObject("projectile")
                .AddComponent<Projectile>()
                .Setup(new Projectile.Settings() { Speed = speed });

            var collider = new GameObject("collider")
                .AddComponent<BoxCollider>();

            collider.transform.position = new Vector3(0, 0f, colliderPos);

            yield return new WaitForSeconds(waitForSeconds);

            Assert.IsTrue(projectile != null);

            Object.DestroyImmediate(projectile.gameObject);
            Object.DestroyImmediate(collider.gameObject);

            yield return null;
        }

        private static IEnumerable<TestCaseData> ShouldNotHitTestCaseSource()
        {
            yield return new TestCaseData(1f, 10f, 1f)
                .SetName("Collider 10 m far and speed 1 m/s for 1 second")
                .Returns(null);
            yield return new TestCaseData(3f, 10f, 3f)
                .SetName("Collider 10 m far and speed 3 m/s for 3 second")
                .Returns(null);
        }

        [UnityTest]
        [RequiresPlayMode]
        [TestCaseSource(nameof(ShouldHitTestCaseSource))]
        public IEnumerator ShouldHitBeforeHitCollider(
            float speed, float colliderPos, float waitForSeconds
        )
        {
            var projectile = new GameObject("projectile")
                .AddComponent<Projectile>()
                .Setup(new Projectile.Settings() { Speed = speed });

            var collider = new GameObject("collider")
                .AddComponent<BoxCollider>();

            collider.transform.position = new Vector3(0, 0f, colliderPos);

            yield return new WaitForSeconds(waitForSeconds);

            Assert.IsTrue(projectile == null);

            Object.DestroyImmediate(collider.gameObject);

            yield return null;
        }

        private static IEnumerable<TestCaseData> ShouldHitTestCaseSource()
        {
            yield return new TestCaseData(2f, 10f, 5f)
                .SetName("Collider 10 m far and speed 2 m/s for 5 second")
                .Returns(null);
            yield return new TestCaseData(5f, 10f, 2f)
                .SetName("Collider 10 m far and speed 5 m/s for 2 second")
                .Returns(null);
            yield return new TestCaseData(5f, 1f, 1f)
                .SetName("Collider 1 m far and speed 5 m/s for 1 second")
                .Returns(null);
        }
    }
}