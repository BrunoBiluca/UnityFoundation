using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Features
{
    public class ObjectPooling : MonoBehaviour, IObjectPooling
    {
        [SerializeField] private bool destroyOnLoad = true;
        [field: SerializeField] public ObjectPoolingSettings Config { get; set; }

        public bool DestroyOnLoad => destroyOnLoad;

        private readonly List<PooledObject> pool = new();

        public void Awake()
        {
            if(!DestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        public void Setup(ObjectPoolingSettings config)
        {
            Config = config;
        }

        public void InstantiateObjects()
        {
            if(pool.Count >= Config.PoolSize) return;

            for(var i = 0; i < Config.PoolSize; i++)
                CreateNewInstance();
        }

        public Optional<PooledObject> GetAvailableObject()
        {
            return TryGetPooledObject().Some(pooledObject => pooledObject.Activate());
        }

        public Optional<PooledObject> GetAvailableObject(Action<GameObject> preActivate)
        {
            return TryGetPooledObject()
                .Some(pooledObject => pooledObject.Activate(preActivate));
        }

        private Optional<PooledObject> TryGetPooledObject()
        {
            var obj = TryGetDeactivePooledObject();

            if(Config.CanGrown)
                obj = Optional<PooledObject>.Some(CreateNewInstance());

            return obj;
        }

        private Optional<PooledObject> TryGetDeactivePooledObject()
        {
            for(var i = 0; i < pool.Count; i++)
            {
                if(!pool[i].IsActive)
                {
                    return Optional<PooledObject>.Some(pool[i]);
                }
            }

            return Optional<PooledObject>.None();
        }

        private PooledObject CreateNewInstance()
        {
            var pooledObject = Instantiate(Config.ObjectPrefab).GetComponent<PooledObject>();

            if(pooledObject == null)
                throw new MissingPooledObjectComponentException();

            pooledObject.ActiveTimeout = Config.ActiveTimeout;
            pooledObject.Setup(DestroyOnLoad).Deactivate();
            pool.Add(pooledObject);
            return pooledObject;
        }
    }
}