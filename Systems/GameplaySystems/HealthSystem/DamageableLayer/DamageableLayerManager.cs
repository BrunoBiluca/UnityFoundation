using UnityFoundation.Code;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public class DamageableLayerManager : Singleton<DamageableLayerManager>
    {
        [SerializeField]
        private DamageableLayerConfigSO config;

        public void Setup(DamageableLayerConfigSO config)
        {
            this.config = config;
        }

        public bool LayerCanDamage(
            DamageableLayer testingLayer,
            params DamageableLayer[] otherLayers
        )
        {
            if(config == null)
                return true;

            foreach(var otherLayer in otherLayers)
            {
                var relationship = config.relationships
                    .Find(r =>
                    (r.layer1 == testingLayer && r.layer2 == otherLayer)
                    || (r.layer1 == otherLayer && r.layer2 == testingLayer)
                );

                if(relationship == null)
                    return false;

                if(!relationship.hasRelation)
                    return false;
            }

            return true;
        }
    }
}