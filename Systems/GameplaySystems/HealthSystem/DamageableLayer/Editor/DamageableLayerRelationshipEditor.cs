using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityFoundation.HealthSystem;

namespace Assets.UnityFoundation.Systems.HealthSystem.HealthSystemEditor
{
    [CustomEditor(typeof(DamageableLayerConfigSO))]
    public class DamageableLayerRelationshipEditor : Editor
    {
        private DamageableLayerConfigSO currentRelationship;

        public void OnEnable()
        {
            currentRelationship = (DamageableLayerConfigSO)target;
        }

        public override void OnInspectorGUI()
        {
            List<UpdateLayerEntity> updateLayers = new List<UpdateLayerEntity>();

            EditorGUILayout.LabelField("Layers");
            for(var i = 0; i < currentRelationship.layers.Count; i++)
            {
                var layer = currentRelationship.layers[i];
                var newLayer = EditorGUILayout.ObjectField(
                    layer.LayerName,
                    layer,
                    typeof(DamageableLayer),
                    false
                ) as DamageableLayer;

                if(layer != newLayer)
                {
                    updateLayers.Add(
                        new UpdateLayerEntity() {
                            index = i,
                            newLayer = newLayer
                        }
                    );
                }
            }

            if(updateLayers.Count > 0)
            {
                foreach(var updateLayer in updateLayers)
                    UpdateLayer(updateLayer);
            }

            if(GUILayout.Button("Add Layer"))
            {
                Undo.RecordObject(currentRelationship, "Add new layer");
                currentRelationship.layers.Add(CreateInstance<DamageableLayer>());
            }

            if(GUILayout.Button("Remove Layer"))
            {
                Undo.RecordObject(currentRelationship, "Remove new layer");
                currentRelationship.layers.Remove(currentRelationship.layers.Last());
            }

            EditorGUILayout.LabelField("Relationships");

            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("");

            var orderedLayers = currentRelationship.layers
                .FindAll(l => l != null)
                .OrderBy(l => l.LayerName);

            foreach(var layerX in orderedLayers)
            {
                EditorGUILayout.LabelField(
                    layerX.LayerName,
                    new GUIStyle() {
                        normal = new GUIStyleState() { textColor = Color.white },
                        contentOffset = new Vector2(-15, 0)
                    },
                    new GUILayoutOption[] {
                    GUILayout.Width(60),
                    GUILayout.Height(30)
                    }
                );
            }
            GUILayout.EndHorizontal();

            foreach(var layerY in orderedLayers)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(layerY.LayerName);

                foreach(var layerX in orderedLayers)
                {
                    var relationship = currentRelationship.relationships
                        .Find(r =>
                            (r.layer1 == layerY && r.layer2 == layerX)
                            || (r.layer1 == layerX && r.layer2 == layerY)
                        );

                    if(relationship == null) continue;

                    var newState = EditorGUILayout.Toggle(
                        relationship.hasRelation,
                        new GUILayoutOption[] {
                        GUILayout.Width(60)
                        }
                    );
                    relationship.hasRelation = newState;
                }
                GUILayout.EndHorizontal();
            }
        }

        private void UpdateLayer(UpdateLayerEntity updateLayer)
        {
            if(currentRelationship.layers.Contains(updateLayer.newLayer))
            {
                Debug.LogWarning(
                    "Damageable Layer already exists on this Damageable Relationship SO"
                );
                return;
            }

            if(updateLayer.newLayer == null)
            {
                var oldLayer = currentRelationship.layers[updateLayer.index];

                Undo.RecordObjects(
                    new Object[] { currentRelationship, oldLayer },
                    "Remove Damageable Layer"
                );

                currentRelationship.relationships
                    .RemoveAll(r => r.layer2 == oldLayer || r.layer1 == oldLayer);

                currentRelationship.layers.RemoveAt(updateLayer.index);
                return;
            }

            Undo.RecordObjects(
                new Object[] { currentRelationship, updateLayer.newLayer },
                "Update Damageable Layer"
            );

            currentRelationship.layers[updateLayer.index] = updateLayer.newLayer;

            foreach(var layer in currentRelationship.layers)
            {
                var firstLayer = currentRelationship.layers[updateLayer.index];

                var rel = CreateInstance<DamageableLayerRelationship>();
                rel.name = $"{firstLayer.LayerName}_{layer.LayerName}";
                rel.layer1 = firstLayer;
                rel.layer2 = layer;
                rel.hasRelation = true;

                currentRelationship.relationships.Add(rel);
            }
        }
    }
}
