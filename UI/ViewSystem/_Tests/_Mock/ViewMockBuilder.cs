using Moq;
using UnityEngine;
using UnityFoundation.TestUtility;
using UnityFoundation.UI.ViewSystem;

namespace UnityFoundation.UI.ViewSystem.Tests
{
    public class TestView : BaseView { }

    public class TestViewBuilder : FakeBuilder<IView>
    {
        protected override IView OnBuild()
        {
            return new GameObject().AddComponent<TestView>();
        }
    }

    public class ViewMockBuilder : MockBuilder<IView>
    {
        protected override Mock<IView> OnBuild()
        {
            var mock = new Mock<IView>();
            var isVisible = false;
            mock.Setup(v => v.Show()).Callback(() => isVisible = true);
            mock.Setup(v => v.Hide()).Callback(() => isVisible = false);
            mock.Setup(v => v.IsVisible).Returns(() => isVisible);
            return mock;
        }
    }
}