using Assets.UnityFoundation.HealthSystem;
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
        private void Awake()
        {
            if(layers == null)
                layers = new List<DamageableLayer>();

            layers.Add(CreateInstance<DamageableLayer>());

            if(relationships == null)
                relationships = new List<DamageableLayerRelationship>();
        }
#endif

    }
}