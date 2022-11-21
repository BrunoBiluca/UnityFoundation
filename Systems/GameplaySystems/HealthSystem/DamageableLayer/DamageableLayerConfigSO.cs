using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UFEC = UnityFoundation.Code.UnityAdapter.UnityFoundationEditorConfig;

namespace UnityFoundation.HealthSystem
{

    [CreateAssetMenu(
        fileName = "damageble_layer_relationship",
        menuName = UFEC.BASE_CONTEXT_MENU_PATH + "HealthSystem/Relationship"
    )]
    public class DamageableLayerConfigSO : ScriptableObject, ISerializationCallbackReceiver
    {
        public List<DamageableLayer> layers;

        public List<DamageableLayerRelationship> relationships;

#if UNITY_EDITOR
        public void Awake()
        {
            Setup();
            layers.Add(CreateInstance<DamageableLayer>());
        }

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
            if(string.IsNullOrEmpty(AssetDatabase.GetAssetPath(this)))
                return;

            foreach(var rel in relationships)
            {
                if(!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(rel)))
                    continue;

                AssetDatabase.AddObjectToAsset(rel, this);
            }
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