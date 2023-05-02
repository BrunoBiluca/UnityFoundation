using System;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.UI.ViewSystem
{
    [Serializable]
    public class ViewComponent : IVisible
    {
        [SerializeField][ShowOnly] private string id;
        [SerializeField] private GameObject comp;

        private Action<GameObject> updateCallback;

        public string Id => id;

        public bool StartVisible { get; set; }

        public ViewComponent(string name, Action<GameObject> updateCallback)
        {
            id = name;
            this.updateCallback = updateCallback;
        }

        public void Show()
        {
            updateCallback(comp);
        }

        public void Hide()
        {
            throw new NotImplementedException();
        }

        public void SetComponent(GameObject component)
        {
            comp = component;
        }

        public Action<GameObject> GetUpdateCallback()
        {
            return updateCallback;
        }

        public void SetUpdateCallback(Action<GameObject> updateCallback)
        {
            this.updateCallback = updateCallback;
        }

        public override bool Equals(object obj)
        {
            if(obj is not ViewComponent comp)
                return false;

            return comp.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            if(id == null) return 0;
            return id.GetHashCode();
        }
    }
}
