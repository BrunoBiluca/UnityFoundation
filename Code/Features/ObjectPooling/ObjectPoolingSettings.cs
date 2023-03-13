using UnityEngine;
using System;

namespace UnityFoundation.Code.Features
{
    [Serializable]
    public class ObjectPoolingSettings
    {
        [field: SerializeField] public GameObject ObjectPrefab { get; set; }
        [field: SerializeField] public int PoolSize { get; set; }
        [field: SerializeField] public bool CanGrown { get; set; }
        [field: SerializeField] public float ActiveTimeout { get; set; } = 1f;
    }
}