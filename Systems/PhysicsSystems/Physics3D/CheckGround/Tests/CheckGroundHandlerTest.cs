using UnityFoundation.Physics3D.CheckGround;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Tools.TimeUtils;
using Moq;

public class CheckGroundHandlerTest
{

    [Test]
    [TestCaseSource(nameof(CollisionCases))]
    public void ShouldBeGroundedWhenCollideWithFloor(
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

        Assert.AreEqual(expected, checkGroundHandler.IsGrounded);

        Object.DestroyImmediate(floor);
        Object.DestroyImmediate(groundedObject);
    }

    [Test]
    public void ShouldDisabledCheckForXTime()
    {
        var floor = new GameObject("floor");
        floor.transform.position = new Vector3(0, 0, 0);
        var floorCol = floor.AddComponent<BoxCollider>();
        floorCol.size = new Vector3(1, 1, 1);

        var groundedObject = new GameObject("groundedObject");
        groundedObject.transform.position = new Vector3(0, 1.5f, 0);
        var objCol = groundedObject.AddComponent<CapsuleCollider>();
        objCol.height = 2f;
        objCol.radius = 0.5f;

        var checkGroundHandler = new CheckGroundHandler(groundedObject.transform, 0.1f);
        checkGroundHandler.DebugMode(true).CheckGround();

        Assert.True(checkGroundHandler.IsGrounded);

        var timerMock = new Mock<ITimer>();
        checkGroundHandler.Disable(timerMock.Object);

        checkGroundHandler.CheckGround();
        Assert.False(checkGroundHandler.IsGrounded);

        timerMock.SetupGet(t => t.Completed).Returns(true);
        Assert.False(checkGroundHandler.IsGrounded);

        checkGroundHandler.CheckGround();
        Assert.True(checkGroundHandler.IsGrounded);

        Object.DestroyImmediate(floor);
        Object.DestroyImmediate(groundedObject);
    }

    private static IEnumerable<TestCaseData> CollisionCases()
    {
        yield return new TestCaseData(1.51f, 0.0f, false)
            .SetName("Object close to the floor with small offset");
        yield return new TestCaseData(1.5f, 0.1f, true)
            .SetName("Object close to the floor with good offset");
        yield return new TestCaseData(1.61f, 0.1f, false)
            .SetName("Floor far away with small offset");
        yield return new TestCaseData(1.6f, 0.2f, true)
            .SetName("Floor far away with bigger offset");
    }

}
