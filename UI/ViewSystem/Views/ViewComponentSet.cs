using System;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.UI.ViewSystem
{
    [Serializable]
    public class ViewComponentSet
    {
        [Serializable]
        private class ViewComponentDictionary : SerializableDictionary<string, ViewComponent> { }

        [SerializeField] private ViewComponentDictionary registry;

        public void RegisterRange(IEnumerable<ViewComponent> components)
        {
            registry ??= new ViewComponentDictionary();
            List<string> allComponentsIds = RegisterComponents(components);
            DeleteNotRegisteredComponents(allComponentsIds);
        }

        private List<string> RegisterComponents(IEnumerable<ViewComponent> components)
        {
            var allComponentsIds = new List<string>();
            foreach(var component in components)
            {
                allComponentsIds.Add(component.Id);
                if(registry.TryGetValue(component.Id, out ViewComponent serializedComponent))
                    serializedComponent.SetUpdateCallback(component.GetUpdateCallback());
                else
                    registry[component.Id] = component;
            }

            return allComponentsIds;
        }

        private void DeleteNotRegisteredComponents(List<string> allComponentsIds)
        {
            var deleteComponents = new List<string>();
            foreach(var componentId in registry.Keys)
            {
                if(!allComponentsIds.Contains(componentId))
                    deleteComponents.Add(componentId);
            }

            foreach(var id in deleteComponents)
                registry.Remove(id);
        }

        public void Show()
        {
            foreach(var component in registry.Values)
                component.Show();
        }
    }
}