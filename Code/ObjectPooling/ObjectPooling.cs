using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Assets.UnityFoundation.Code.ObjectPooling.ObjectPoolingStrings;

namespace Assets.UnityFoundation.Code.ObjectPooling
{
    public class ObjectPooling : MonoBehaviour
    {

        [SerializeField] private GameObject objectPrefab;
        [SerializeField] private int poolSize;
        [SerializeField] private bool canGrown;

        public bool DestroyOnLoad { get; set; }

        private readonly List<PooledObject> pool = new List<PooledObject>();

        private void Awake()
        {
            SceneManager.sceneLoaded += delegate {
                pool.ForEach(obj => obj.Deactivate());
            };
        }

        public void InstantiateObjects()
        {
            for(int i = 0; i < poolSize; i++)
            {
                var pooledObject = Instantiate(objectPrefab)
                    .GetComponent<PooledObject>();

                if(pooledObject == null)
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