using Assets.UnityFoundation.Code.Common;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public class DamageableLayerManager : Singleton<DamageableLayerManager>
    {
        [SerializeField]
        private DamageableLayerConfigSO config;

        public bool HasRelationship(
            DamageableLayer layer,
            params DamageableLayer[] otherLayers
        )
        {
            if(config == null) 
                return true;

            foreach(var otherLayer in otherLayers)
            {
                var relationship = config.relationships
                    .Find(r =>
                    (r.layer1 == layer && r.layer2 == otherLayer)
                    || (r.layer1 == otherLayer && r.layer2 == layer)
                );

                if(relationship == null) 
                    return false;
            }

            return true;
        }
    }
}