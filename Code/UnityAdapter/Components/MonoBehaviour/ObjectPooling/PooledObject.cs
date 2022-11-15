using UnityFoundation.Code.UnityAdapter;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.ObjectPooling
{
    public class PooledObject : MonoBehaviour, IDestroyBehaviour
    {
        public event Action OnDestroyAction;

        public bool IsActive => gameObject.activeInHierarchy;

        public string Tag { get; set; }

        public virtual PooledObject Setup(bool destroyOnLoad)
        {
            if(!destroyOnLoad) DontDestroyOnLoad(gameObject);
            return this;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Activate(Action<GameObject> preActivate)
        {
            preActivate(gameObject);
            Activate();
        }

        public void Activate<T>(Action<T> preActivate) where T : MonoBehaviour
        {
            preActivate(GetComponent<T>());
            gameObject.SetActive(true);
        }

        public virtual PooledObject Deactivate()
        {
            OnDestroyAction?.Invoke();
            gameObject.SetActive(false);
            return this;
        }

        public IEnumerator Deativate(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            Deactivate();
        }

        public void Destroy()
        {
            Deactivate();
        }

        public void Destroy(float time)
        {
            StartCoroutine(Deativate(time));
        }

        public void OnBeforeDestroy(Action p)
        {
            OnDestroyAction = p;
        }

        ///
        ///   UNITY METHODS
        ///

        public void OnDestroy()
        {
            if(PlayState.IsPlayMode)
                Debug.LogWarning(
                    "Pooled object was destroyed. Is this the expected behaviour?"
                );
        }
    }
}