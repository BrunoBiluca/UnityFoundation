using UnityFoundation.Code.PhysicsUtils;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class CheckGroundHandlerTest
{

    [UnityTest]
    [TestCaseSource(nameof(CollisionCases))]
    public IEnumerator ShouldBeGroundedWhenCollideWithFloor(
        float objPos, float objOff, bool expected
    )
    {
        var floor = new GameObject("floor");
        floor.transform.position = new Vector3(0, 0, 0);
        var floorCol = floor.AddComponent<BoxCollider>();
        floorCol.size = new Vector3(1, 1, 1);

        var groundedObject = new GameObject("groundedObject");
        groundedObject.transform.position = new Vector3(0, objPos, 0);
        var objCol = groundedObject.AddComponent<CapsuleCollider>();
        objCol.height = 2f;
        objCol.radius = 0.5f;

        var checkGroundHandler = new CheckGroundHandler(groundedObject.transform, objOff);
        checkGroundHandler.DebugMode(true).CheckGround();

        yield return null;

        Assert.AreEqual(expected, checkGroundHandler.IsGrounded);

        Object.DestroyImmediate(floor);
        Object.DestroyImmediate(groundedObject);
    }

    private static IEnumerable<TestCaseData> CollisionCases()
    {
        yield return new TestCaseData(1.51f, 0.0f, false)
            .SetName("Object close to the floor with small offset")
            .Returns(null);
        yield return new TestCaseData(1.5f, 0.1f, true)
            .SetName("Object close to the floor with good offset")
            .Returns(null);
        yield return new TestCaseData(1.61f, 0.1f, false)
            .SetName("Floor far away with small offset")
            .Returns(null);
        yield return new TestCaseData(1.6f, 0.2f, true)
            .SetName("Floor far away with bigger offset")
            .Returns(null);
    }

}
