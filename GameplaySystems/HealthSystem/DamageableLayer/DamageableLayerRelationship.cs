using UnityEngine;

namespace UnityFoundation.HealthSystem
{
    public class DamageableLayerRelationship : ScriptableObject
    {
        public DamageableLayer layer1;
        public DamageableLayer layer2;
        public bool hasRelation;
    }
}