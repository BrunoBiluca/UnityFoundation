using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityFoundation.TestUtility
{
    public class AssetLoaderHelper
    {
        private readonly string path;

        public AssetLoaderHelper(string path)
        {
            this.path = path;
        }

        public MonoAsset Load()
        {
            var assetPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if(assetPrefab == null)
                throw new AssetNotFound(path);

            return new(UnityEngine.Object.Instantiate(assetPrefab));
        }
    }

    public class MonoAsset : IDisposable
    {
        private readonly GameObject obj;

        public MonoAsset(GameObject obj)
        {
            this.obj = obj;
        }

        public T GetComponent<T>() where T : MonoBehaviour
        {
            return obj.GetComponent<T>();
        }

        public void Dispose()
        {
            UnityEngine.Object.DestroyImmediate(obj, true);
        }
    }

    public class AssetNotFound : Exception
    {
        const string msg = "Asset not found on path: <path>";
        public AssetNotFound(string path) : base(msg.Replace("<path>", path)) 
        { }
    }
}