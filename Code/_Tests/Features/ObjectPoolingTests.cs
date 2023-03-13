using NUnit.Framework;
using UnityEngine;
using UnityFoundation.Code.Features;

namespace UnityFoundation.Code.Tests
{
    public class ObjectPoolingTests
    {
        [Test]
        public void Should_instantiate_the_exact_amount_of_objects()
        {
            var pooledObject = new GameObject("pooled_object").AddComponent<PooledObject>();
            var objectPooling = new GameObject("object_pooling").AddComponent<ObjectPooling>();

            objectPooling.Setup(new ObjectPoolingSettings() {
                ObjectPrefab = pooledObject.gameObject,
                PoolSize = 3,
                CanGrown = false
            });

            objectPooling.InstantiateObjects();

            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.True);
            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.True);
            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.True);
            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.False);
        }

        [Test]
        public void Should_continues_instantiating_objects_where_can_grown_is_active()
        {
            var pooledObject = new GameObject("pooled_object").AddComponent<PooledObject>();
            var objectPooling = new GameObject("object_pooling").AddComponent<ObjectPooling>();

            objectPooling.Setup(new ObjectPoolingSettings() {
                ObjectPrefab = pooledObject.gameObject,
                PoolSize = 3,
                CanGrown = true
            });

            objectPooling.InstantiateObjects();

            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.True);
            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.True);
            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.True);
            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.True);
            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.True);
            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.True);
            Assert.That(objectPooling.GetAvailableObject().IsPresent, Is.True);
        }
    }
}
