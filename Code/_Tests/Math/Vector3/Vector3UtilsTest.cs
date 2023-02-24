using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Math.Tests
{
    public class Vector3UtilsTest : MonoBehaviour
    {
        [TestCaseSource(nameof(Vector3CenterTestSource))]
        public void Should_expect_center_to_be(
            Vector3 a, Vector3 b, Vector3 expected
        )
        {
            Assert.That(Vector3Utils.Center(a, b), Is.EqualTo(expected));
        }

        private static IEnumerable<TestCaseData> Vector3CenterTestSource()
        {
            yield return new TestCaseData(Vector3.one, Vector3.one, Vector3.one)
                .SetName("equal to a and b when points are the same");

            yield return new TestCaseData(Vector3.one, -Vector3.one, Vector3.zero)
                .SetName("equal to zero when points are one inversion of the other");

            yield return new TestCaseData(
                Vector3.one, Vector3.one * 2, new Vector3(1.5f, 1.5f, 1.5f)
            )
                .SetName("equal to the mean when arbitrary positive points");

            yield return new TestCaseData(
                -Vector3.one, -Vector3.one * 2, new Vector3(-1.5f, -1.5f, -1.5f)
            )
                .SetName("equal to the mean when arbitrary negative points");
        }
    }
}
