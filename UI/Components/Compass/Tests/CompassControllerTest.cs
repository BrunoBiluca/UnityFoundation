using NUnit.Framework;
using UnityEngine;

namespace UnityFoundation.Compass.Tests
{
    public class CompassControllerTest
    {
        [Test]
        public void ShouldThrowArgumentNullExceptionIfPlayerIsNotSetup()
        {
            Assert.Throws<System.ArgumentException>(() => new CompassController(null));
        }

        [Test]
        public void ShouldNotDisplayWhenThereIsNotObjectRegistered()
        {
            var compass = BuildCompass(out _);

            Assert.AreEqual(0, compass.CompassObjects.Count);
        }

        [Test]
        public void ShouldDisplayObjectsRelatedToPlayerPosition()
        {
            var compass = BuildCompass(out Transform player);

            var objInFront = BuildObjectInFrontOfPlayer(player);
            compass.Register(objInFront);

            var obj45FromPlayer = BuildObject45DegreesFromPlayer(player);
            compass.Register(obj45FromPlayer);

            var objMinus45 = BuildObjectMinus45DegreesFromPlayer(player);
            compass.Register(objMinus45);

            var objInBack = BuildObjectInBackOfPlayer(player);
            compass.Register(objInBack);

            compass.Update();

            Assert.AreEqual(4, compass.CompassObjects.Count);

            Assert.AreEqual(0f, compass.CompassObjects[objInFront].Angle);
            Assert.AreEqual(1f, compass.CompassObjects[objInFront].Distance);

            Assert.AreEqual(45f, compass.CompassObjects[obj45FromPlayer].Angle);
            Assert.AreEqual(Mathf.Sqrt(2f), compass.CompassObjects[obj45FromPlayer].Distance);

            Assert.AreEqual(-45f, compass.CompassObjects[objMinus45].Angle);
            Assert.AreEqual(Mathf.Sqrt(2f), compass.CompassObjects[objMinus45].Distance);
            
            Assert.AreEqual(90f, compass.CompassObjects[objInBack].Angle);
            Assert.AreEqual(1f, compass.CompassObjects[objInBack].Distance);
        }

        private Transform BuildObjectInBackOfPlayer(Transform player)
        {
            var obj = new GameObject("obj").transform;
            obj.position = player.transform.position - player.transform.forward;

            return obj;
        }

        private Transform BuildObjectMinus45DegreesFromPlayer(Transform player)
        {
            var obj = new GameObject("obj").transform;
            obj.position = player.transform.position
                + player.transform.forward
                - player.transform.right;

            return obj;
        }

        private Transform BuildObject45DegreesFromPlayer(Transform player)
        {
            var obj = new GameObject("obj").transform;
            obj.position = player.transform.position
                + player.transform.forward
                + player.transform.right;

            return obj;
        }

        private Transform BuildObjectInFrontOfPlayer(Transform player)
        {
            var obj = new GameObject("obj").transform;
            Vector3 oneMeterAheadPlayer = player.transform.position + player.transform.forward;
            obj.position = oneMeterAheadPlayer;
            return obj;
        }

        private static CompassController BuildCompass(out Transform player)
        {
            player = new GameObject("player").transform;
            return new CompassController(player);
        }

    }
}