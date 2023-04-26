using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public abstract class ComponentHolder<T> : MonoBehaviour
    {
        public T Instance { get; private set; }

        void Awake()
        {
            Instance = Create();
        }

        void Update()
        {
            if(Instance is IUpdatable updatable)
                updatable.Update(Time.deltaTime);
        }

        protected abstract T Create();
    }
}