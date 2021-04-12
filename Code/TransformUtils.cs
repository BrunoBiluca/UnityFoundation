using Assets.UnityFoundation.TimeUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformUtils {

    public static void RemoveChildObjects(Transform parent) {
        var children = new List<GameObject>();

        foreach(Transform child in parent) {
            children.Add(child.gameObject);
        }

        foreach(var child in children) {
            Object.Destroy(child);
        }
    }

    public static IEnumerator RemoveChildObjects(Transform parent, float waitBetween) {
        var children = new List<GameObject>();

        foreach(Transform child in parent) {
            if(child.CompareTag(Tags.echoes))
                children.Add(child.gameObject);
        }

        foreach(var child in children) {
            Object.Destroy(child);
            yield return WaittingCoroutine.RealSeconds(waitBetween);
        }
    }

}
