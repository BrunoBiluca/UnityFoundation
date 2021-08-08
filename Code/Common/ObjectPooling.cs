using System;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour {

    public bool IsActive => gameObject.activeInHierarchy;

    protected virtual void Setup() { }

    public void Activate(Action<GameObject> preActivate) {
        preActivate(gameObject);
        gameObject.SetActive(true);
    }

    public void Activate<T>(Action<T> preActivate) where T : MonoBehaviour {
        preActivate(GetComponent<T>());
        gameObject.SetActive(true);
    }

    public virtual PooledObject Deactivate()
    {
        gameObject.SetActive(false);
        return this;
    }
}

public class ObjectPooling : MonoBehaviour {

    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private bool canGrown;

    private readonly List<PooledObject> pool = new List<PooledObject>();

    void Awake() {
        for(int i = 0; i < poolSize; i++) {
            var pooledObject = Instantiate(objectPrefab)
                .GetComponent<PooledObject>()
                .Deactivate();
            pool.Add(pooledObject);
        }
    }

    public PooledObject GetAvailableObject() {
        for(int i = 0; i < poolSize; i++) {
            if(!pool[i].IsActive)
                return pool[i];
        }

        if(canGrown) {
            var pooledObject = Instantiate(objectPrefab)
                .GetComponent<PooledObject>()
                .Deactivate();
            pool.Add(pooledObject);
            return pooledObject;
        }

        return null;
    }

}
