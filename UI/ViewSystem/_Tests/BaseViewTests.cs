using NUnit.Framework;
using UnityEngine;
using UnityFoundation.UI.ViewSystem;

namespace UnityFoundation.UI.ViewSystem.Tests
{
    public class BaseViewTests
    {
        public class TestView : BaseView
        {
            public bool WasAwaked { get; private set; }
            public bool WasFirstShown { get; private set; }

            protected override void OnAwake() => WasAwaked = true;

            protected override void OnFirstShow() => WasFirstShown = true;
        }

        [Test]
        public void Should_call_awake_on_first_show_when_as_not_awake_before()
        {
            var testView = new GameObject("view").AddComponent<TestView>();
            testView.Show();

            Assert.That(testView.WasAwaked, Is.True);
        }

        [Test]
        public void Should_call_first_show_only_on_first_call()
        {
            var testView = new GameObject("view").AddComponent<TestView>();
            testView.Show();

            Assert.That(testView.WasFirstShown, Is.True);
        }
    }
}