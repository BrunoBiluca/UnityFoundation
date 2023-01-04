using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityFoundation.Code
{
    public static class GameObjectUtils
    {
        public static T FindInScene<T>()
        {
            foreach(var obj in Object.FindObjectsOfType<MonoBehaviour>())
                if(obj is T t) return t;

            return default;
        }
    }
}
