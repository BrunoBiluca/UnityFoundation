using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>{

    private static Singleton<T> instance;
    public static T Instance {
        get {
            return (T)instance;
        }
    }

    [SerializeField] private bool destroyOnLoad;

    private void Awake() {
        if(instance == null) {
            instance = this;
            if(!destroyOnLoad) DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        OnAwake();
    }

    protected virtual void OnAwake() {}

}
