using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PooledObject : MonoBehaviour {

    public bool IsActive => gameObject.activeInHierarchy;

    public virtual PooledObject Setup(bool destroyOnLoad) {
        if(!destroyOnLoad) DontDestroyOnLoad(gameObject);
        return this;
    }

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

    public void Destroy()
    {
        throw new DestroyPooledObjectException("Destroy pooled object is not allow.");
    }
}
