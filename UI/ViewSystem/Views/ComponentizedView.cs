using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.UI.ViewSystem
{
    [ExecuteInEditMode]
    public abstract class ComponentizedView : BaseView
    {
        [SerializeField] private ViewComponentSet componentSet;

        protected override void PreAwake()
        {
            CreateOrUpdateComponents();
        }

        protected override void OnFirstShow()
        {
            CreateOrUpdateComponents();
            componentSet.Show();
        }

        protected override void OnShow() => componentSet.Show();

        public void Update()
        {
            if(!Application.isPlaying)
                CreateOrUpdateComponents();
        }

        private void CreateOrUpdateComponents()
        {
            componentSet ??= new ViewComponentSet();
            componentSet.RegisterRange(RegisterComponents());
        }

        protected abstract IEnumerable<ViewComponent> RegisterComponents();
    }
}