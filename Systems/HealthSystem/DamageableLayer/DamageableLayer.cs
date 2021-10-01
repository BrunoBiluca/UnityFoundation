using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.HealthSystem
{
    [CreateAssetMenu(
        fileName = "damageable_layer", 
        menuName = "HealthSystem/DamageableLayer"
    )]
    public class DamageableLayer : ScriptableObject
    {
        [SerializeField] private string layerName;

        public string LayerName => layerName;

        private List<DamageableLayer> InteractiveLayers { get; }
            = new List<DamageableLayer>();

        private void AddInteraction(DamageableLayer layer)
        {
            if(InteractiveLayers.Contains(layer))
                return;

            InteractiveLayers.Add(layer);
        }

        public bool HasInteraction(DamageableLayer layer)
        {
            return InteractiveLayers.Contains(layer);
        }
    }
}