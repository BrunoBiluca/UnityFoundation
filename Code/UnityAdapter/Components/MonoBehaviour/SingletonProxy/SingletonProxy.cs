using UnityEngine;

namespace UnityFoundation.Code
{
    public class SingletonProxy<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static T I => Instance;
        public static T Instance {
            get {
                if(instance == null)
                    CreateInstance();

                return instance;
            }
        }

        private static void CreateInstance()
        {
            var className = typeof(T).Name;
            instance = new GameObject(className).AddComponent<T>();
            Debug.LogWarning($"{className} was created automatically and was not found on scene. Is this the expected behaviour?");
        }

        public void Awake()
        {
            if(instance == null)
                CreateInstance();
        }
    }
}