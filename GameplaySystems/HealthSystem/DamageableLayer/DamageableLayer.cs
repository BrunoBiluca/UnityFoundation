using System.Collections.Generic;
using UnityEngine;
using UFEC = UnityFoundation.Code.UnityAdapter.UnityFoundationEditorConfig;

namespace UnityFoundation.HealthSystem
{
    [CreateAssetMenu(
        fileName = "damageable_layer",
        menuName = UFEC.BASE_CONTEXT_MENU_PATH + "HealthSystem/DamageableLayer"
    )]
    public class DamageableLayer : ScriptableObject
    {
        [field: SerializeField] public string LayerName { get; set; }

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