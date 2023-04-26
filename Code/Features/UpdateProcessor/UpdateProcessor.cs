using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code
{
    public class UpdateProcessor : MonoBehaviour
    {
        public static UpdateProcessor Create()
        {
            return new GameObject("update_processor").AddComponent<UpdateProcessor>();
        }

        readonly Dictionary<Guid, IUpdatable> registry = new();
        readonly Dictionary<Guid, IUpdatable> toBeAdded = new();
        readonly List<Guid> toBeRemoved = new();

        public Guid Register(IUpdatable updatable)
        {
            var id = Guid.NewGuid();
            toBeAdded[id] = updatable;
            return id;
        }

        public Guid Register(Action<float> updateableCallback)
        {
            return Register(new UpdatableCallback(updateableCallback));
        }

        public void Update()
        {
            foreach(var id in toBeRemoved)
            {
                registry.Remove(id);
            }
            toBeRemoved.Clear();

            foreach(var added in toBeAdded)
            {
                registry[added.Key] = added.Value;
            }
            toBeAdded.Clear();

            foreach(var updatable in registry.Values)
            {
                updatable.Update(Time.deltaTime);
            }
        }

        public void Unregister(Guid processId)
        {
            toBeRemoved.Add(processId);
        }
    }
}