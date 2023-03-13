using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Features
{
    [Serializable]
    public class ObjectPoolingDictionary : SerializableDictionary<string, GameObject> { }

    public class MultipleObjectPooling : MonoBehaviour
    {
        [SerializeField] private ObjectPoolingDictionary objects;
        [SerializeField] private int poolSize;
        [SerializeField] private bool canGrown;

        public bool DestroyOnLoad { get; set; } = true;

        private List<PooledObject> pool;

        private void Awake()
        {
            pool = new List<PooledObject>();
        }

        public void InstantiateObjects()
        {
            foreach(var obj in objects)
            {
                for(int i = 0; i < poolSize; i++)
                {
                    var pooledObject = Instantiate(obj.Value).GetComponent<PooledObject>();

                    if(pooledObject == null)
                        throw new MissingPooledObjectComponentException(obj.Key);

                    pooledObject.GetComponent<PooledObject>()
                        .Setup(DestroyOnLoad)
                        .Deactivate();

                    pooledObject.Tag = obj.Key;
                    pool.Add(pooledObject);
                }
            }
        }

        public Optional<PooledObject> GetAvailableObject(string tag)
        {
            for(int i = 0; i < pool.Count; i++)
            {
                if(pool[i].Tag == tag && !pool[i].IsActive)
                    return Optional<PooledObject>.Some(pool[i]);
            }

            if(canGrown)
            {
                var pooledObject = Instantiate(objects[tag])
                    .GetComponent<PooledObject>()
                    .Setup(DestroyOnLoad)
                    .Deactivate();

                pooledObject.Tag = tag;
                pool.Add(pooledObject);
                return Optional<PooledObject>.Some(pooledObject);
            }

            return Optional<PooledObject>.None();
        }
    }
}