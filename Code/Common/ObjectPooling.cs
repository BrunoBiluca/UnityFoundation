using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject {
    public GameObject Obj { get; private set; }

    public PooledObject(GameObject objectPrefab) {
        Obj = objectPrefab;
    }


    public void Active(Action<GameObject> preActivate) {
        preActivate(Obj);
        Obj.SetActive(true);
    }

}

public class ObjectPooling : MonoBehaviour {

    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private bool canGrown;

    private readonly List<PooledObject> pool = new List<PooledObject>();

    void Awake() {
        for(int i = 0; i < poolSize; i++) {
            var pooledObject = Instantiate(objectPrefab);
            pooledObject.SetActive(false);
            pool.Add(new PooledObject(pooledObject));
        }
    }

    public PooledObject GetAvailableObject() {
        for(int i = 0; i < poolSize; i++) {
            if(!pool[i].Obj.activeInHierarchy)
                return pool[i];
        }

        if(canGrown) {
            var pooledObject = Instantiate(objectPrefab);
            pooledObject.SetActive(false);
            pool.Add(new PooledObject(pooledObject));
        }

        return null;
    }

}
