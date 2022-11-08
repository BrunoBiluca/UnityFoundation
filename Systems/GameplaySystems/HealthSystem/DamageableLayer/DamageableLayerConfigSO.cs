using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.HealthSystem
{
    [Serializable]
    public class DamageableLayerRelationship
    {
        public DamageableLayer layer1;
        public DamageableLayer layer2;
        public bool hasRelation;
    }

    [CreateAssetMenu(
        fileName = "damageble_layer_relationship",
        menuName = "HealthSystem/Relationship"
    )]
    public class DamageableLayerConfigSO : ScriptableObject
    {
        public List<DamageableLayer> layers;

        public List<DamageableLayerRelationship> relationships;

#if UNITY_EDITOR
        public void Awake()
        {
            Setup();
            layers.Add(CreateInstance<DamageableLayer>());
        }

        public void OnEnable()
        {
            Setup();
        }
#endif

        public void Setup()
        {
            layers ??= new List<DamageableLayer>();
            relationships ??= new List<DamageableLayerRelationship>();
        }

    }
}