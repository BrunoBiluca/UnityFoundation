using NUnit.Framework;
using UnityEngine;

namespace UnityFoundation.Code.Tests
{
    public class TransformExtensionsTests
    {
        [Test]
        public void Given_child_exists_should_return_its_component()
        {
            var parent = new GameObject("parent").transform;

            var child = new GameObject("child");
            child.transform.parent = parent;

            var childComp = parent.FindComponent<Transform>("child");

            Assert.That(childComp, Is.Not.Null);
            Assert.That(childComp.name, Is.EqualTo("child"));
        }


        [Test]
        public void Given_children_exist_should_return_all_children_components()
        {
            var parent = new GameObject("parent").transform;

            var child = new GameObject("child");
            child.transform.parent = parent;

            var child2 = new GameObject("child");
            child2.transform.parent = parent;

            var children = parent.FindComponentsInChildren<Transform>();

            Assert.That(children.Length, Is.EqualTo(2));
            Assert.That(children[0].gameObject.name, Is.EqualTo("child"));
            Assert.That(children[1].gameObject.name, Is.EqualTo("child"));
        }
    }
}
