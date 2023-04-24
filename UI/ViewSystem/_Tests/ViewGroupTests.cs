using NUnit.Framework;
using UnityEngine;

namespace UnityFoundation.UI.ViewSystem.Tests
{

    public class ViewGroupTests : MonoBehaviour
    {
        [Test]
        public void Should_show_all_registered_views()
        {
            var group = new ViewsGroup();

            var first = new ViewMockBuilder().Build();
            group.Register(first);

            var second = new ViewMockBuilder().Build();
            group.Register(second);

            var third = new ViewMockBuilder().Build();
            group.Register(third);

            group.Show();

            Assert.That(first.IsVisible, Is.True);
            Assert.That(second.IsVisible, Is.True);
            Assert.That(third.IsVisible, Is.True);
        }
    }
}