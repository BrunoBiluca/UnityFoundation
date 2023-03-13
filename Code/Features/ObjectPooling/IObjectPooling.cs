using System;
using UnityEngine;

namespace UnityFoundation.Code.Features
{
    public interface IObjectPooling
    {
        Optional<PooledObject> GetAvailableObject();
        Optional<PooledObject> GetAvailableObject(Action<GameObject> preActivate);
        void InstantiateObjects();
    }
}