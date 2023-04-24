using NUnit.Framework;

namespace UnityFoundation.UI.ViewSystem.Tests
{
    public class ExclusiveViewGroupTests
    {
        [Test]
        public void Should_show_only_main_view()
        {
            var group = new ExclusiveViewGroup();

            var first = new TestViewBuilder().Build();
            group.RegisterMain(first);

            var second = new TestViewBuilder().Build();
            second.Show();
            group.Register(second);

            var third = new TestViewBuilder().Build();
            third.Show();
            group.Register(third);

            first.Show();

            Assert.That(first.IsVisible, Is.True);
            Assert.That(second.IsVisible, Is.False);
            Assert.That(third.IsVisible, Is.False);
        }

        [Test]
        public void Should_not_show_other_view_when_main_is_visible()
        {
            var group = new ExclusiveViewGroup();

            var first = new TestViewBuilder().Build();
            group.RegisterMain(first);

            var second = new TestViewBuilder().Build();
            group.Register(second);

            var third = new TestViewBuilder().Build();
            group.Register(third);

            first.Show();
            second.Show();
            third.Show();

            Assert.That(first.IsVisible, Is.True);
            Assert.That(second.IsVisible, Is.False);
            Assert.That(third.IsVisible, Is.False);
        }
    }
}