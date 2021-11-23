using Assets.UnityFoundation.Code.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Assets.UnityFoundation.Systems.ObjectPooling.ObjectPoolingStrings;

namespace Assets.UnityFoundation.Systems.ObjectPooling
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] private GameObject objectPrefab;
        [SerializeField] private int poolSize;
        [SerializeField] private bool canGrown;
        [SerializeField] private bool destroyOnLoad = true;

        public bool DestroyOnLoad => destroyOnLoad;

        private List<PooledObject> pool;

        private void Awake()
        {
            if(!DestroyOnLoad) DontDestroyOnLoad(gameObject);

            pool = new List<PooledObject>();
        }

        public void InstantiateObjects()
        {
            if(pool.Count >= poolSize) return;

            for(int i = 0; i < poolSize; i++)
            {
                var obj = Instantiate(objectPrefab);

                if(!obj.TryGetComponent(out PooledObject pooledObject))
                    throw new MissingComponentException(
                        MISSING_POOLED_OBJECT_COMPONENT_MSG()
                    );
                    
                pooledObject
                    .Setup(DestroyOnLoad)
                    .Deactivate();
                pool.Add(pooledObject);
            }
        }

        public Optional<PooledObject> GetAvailableObject()
        {
            for(int i = 0; i < pool.Count; i++)
            {
                if(!pool[i].IsActive)
                    return Optional<PooledObject>.Some(pool[i]);
            }

            if(canGrown)
            {
                var pooledObject = Instantiate(objectPrefab)
                    .GetComponent<PooledObject>()
                    .Setup(DestroyOnLoad)
                    .Deactivate();
                pool.Add(pooledObject);
                return Optional<PooledObject>.Some(pooledObject);
            }

            return Optional<PooledObject>.None();
        }

    }
}