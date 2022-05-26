using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnityFoundation.Radar.Tests
{
    public class RadarViewTest
    {
        string radarPrefabTest = "Assets/UnityFoundation/UI/Components/Radar/Prefabs/radar_canvas.prefab";

        [Test]
        public void ShouldThrowsErrorWhenPlayerWasNotSetup()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(radarPrefabTest);
            var radar = Object.Instantiate(prefab).GetComponent<RadarView>();

            radar.Awake();
            Assert.Throws<System.ArgumentException>(() => radar.Setup(null));
        }

        [Test]
        public void ShouldOnlyDisplayPlayerWhenThereNoEnemies()
        {
            RadarView radar = BuildValidRadar();

            Assert.IsTrue(radar.PlayerRef.gameObject.activeInHierarchy);
        }

        [Test]
        public void ShouldDisplayXObjectsWhenXObjectsWereRegister()
        {
            RadarView radar = BuildValidRadar();

            radar.Register(new GameObject("obj").transform);
            radar.Register(new GameObject("obj").transform);

            Assert.AreEqual(2, radar.ActiveTrackObjects);
        }

        [UnityTest]
        [RequiresPlayMode]
        public IEnumerator ShouldStopDisplayingWhenObjectsAreUnRegistered()
        {
            RadarView radar = BuildValidRadar();

            yield return null;

            var obj1 = new GameObject("obj").transform;
            radar.Register(obj1);
            var obj2 = new GameObject("obj").transform;
            radar.Register(obj2);

            Assert.AreEqual(2, radar.ActiveTrackObjects);

            radar.UnRegister(obj1);
            radar.UnRegister(obj2);
        }

        [Test]
        public void ShouldThrowExceptionWhenRegisterObjectReferencePrefabDoesntHaveRectTrasform()
        {
            RadarView radar = BuildValidRadar();

            Assert.Throws<MissingComponentException>(
                () => radar.Register(new GameObject("obj").transform, new GameObject("obj_ref"))
            );
        }

        [Test]
        public void ShouldRegisterObjectWithDifferentObjectReferencePrefab()
        {
            RadarView radar = BuildValidRadar();

            var objRefPrefab = new GameObject("obj_ref");
            objRefPrefab.AddComponent<RectTransform>();
            radar.Register(new GameObject("obj").transform, objRefPrefab);

            Assert.AreEqual(1, radar.ActiveTrackObjects);
        }

        private RadarView BuildValidRadar()
        {
            var player = new GameObject("player");
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(radarPrefabTest);
            var radar = Object.Instantiate(prefab).GetComponent<RadarView>();

            radar.Awake();
            radar.Setup(player.transform);
            return radar;
        }
    }
}