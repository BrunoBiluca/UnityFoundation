using Assets.UnityFoundation.Systems.HealthSystem;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.HealthSystem.Tests
{
    public class DamageableLayerTests : MonoBehaviour
    {
        [Test]
        public void Should_not_have_damange_relationship_when_no_layer_has_relation()
        {
            var damageLayerManager = new GameObject("damageable_manager")
                .AddComponent<DamageableLayerManager>();
            damageLayerManager.Awake();

            var relationship = ScriptableObject.CreateInstance<DamageableLayerConfigSO>();

            var layer1 = ScriptableObject.CreateInstance<DamageableLayer>();
            var layer2 = ScriptableObject.CreateInstance<DamageableLayer>();

            damageLayerManager.Setup(relationship);

            var result = damageLayerManager.LayerCanDamage(layer1, layer2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_have_damange_relationship_when_layers_has_relation()
        {
            var damageLayerManager = new GameObject("damageable_manager")
                .AddComponent<DamageableLayerManager>();
            damageLayerManager.Awake();

            var config = ScriptableObject.CreateInstance<DamageableLayerConfigSO>();

            var layer1 = ScriptableObject.CreateInstance<DamageableLayer>();
            var layer2 = ScriptableObject.CreateInstance<DamageableLayer>();

            config.relationships.Add(new DamageableLayerRelationship() {
                layer1 = layer1,
                layer2 = layer2,
                hasRelation = true
            });

            damageLayerManager.Setup(config);

            var result = damageLayerManager.LayerCanDamage(layer1, layer2);

            Assert.That(result, Is.True);
        }
    }
}