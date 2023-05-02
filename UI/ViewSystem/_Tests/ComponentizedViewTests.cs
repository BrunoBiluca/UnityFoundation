using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.UI.ViewSystem.Tests
{
    public class ComponentizedViewTests
    {
        public class TestComponentizedView : ComponentizedView
        {
            protected override IEnumerable<ViewComponent> RegisterComponents()
            {
                ViewComponent viewComponent = new("test_component", go => go.SetActive(false));
                viewComponent.SetComponent(transform.Find("component").gameObject);
                yield return viewComponent;
            }
        }

        [Test]
        public void Should_update_component_on_show()
        {
            var testView = new GameObject("test_componentized_view");

            var component = new GameObject("component");
            component.transform.SetParent(testView.transform);

            var view = testView.AddComponent<TestComponentizedView>();

            view.Show();

            Assert.That(component.activeInHierarchy, Is.False);
        }
    }
}
