using UnityFoundation.Code.UnityAdapter;
using System;
using System.Collections;
using UnityEngine;

namespace UnityFoundation.Code.Features
{
    public class PooledObject : MonoBehaviour, IDestroyBehaviour
    {
        public event Action OnDestroyAction;

        public bool IsActive => gameObject.activeInHierarchy;

        public string Tag { get; set; }
        public float ActiveTimeout { get; set; }

        public virtual PooledObject Setup(bool destroyOnLoad)
        {
            if(!destroyOnLoad) DontDestroyOnLoad(gameObject);
            return this;
        }

        public void Activate()
        {
            OnActivate();
            gameObject.SetActive(true);

            if(ActiveTimeout > 0f)
                Destroy(ActiveTimeout);
        }

        public void Activate(Action<GameObject> preActivate)
        {
            preActivate(gameObject);
            Activate();
        }

        public void Activate<T>(Action<T> preActivate) where T : MonoBehaviour
        {
            preActivate(GetComponent<T>());
            Activate();
        }

        public virtual PooledObject Deactivate()
        {
            gameObject.SetActive(false);
            return this;
        }

        public void Destroy()
        {
            OnDestroyAction?.Invoke();
            Deactivate();
        }

        public void Destroy(float time)
        {
            StartCoroutine(Deativate(time));
        }

        public IEnumerator Deativate(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            Deactivate();
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

        protected virtual void OnActivate() { }
    }
}