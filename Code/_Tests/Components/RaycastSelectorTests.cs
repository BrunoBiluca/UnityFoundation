using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code.Tests
{
    public class RaycastSelectorTests
    {
        [Test]
        public void Should_select_anything_when_no_object_is_returned_by_raycast()
        {
            var raycast = new RaycastHandlerMockBuilder().Build();
            var selector = new RaycastSelector(raycast);

            var selected = selector.Select(Vector3.zero);

            Assert.That(selected.IsPresent, Is.False);
            Assert.That(selector.CurrentUnit.IsPresent, Is.False);
        }

        [Test]
        public void Given_object_was_selected_should_unselected()
        {
            var raycast = new RaycastHandlerMockBuilder()
                .WithReturnedObject<ISelectable>()
                .Build();

            var selector = new RaycastSelector(raycast);

            selector.Select(Vector3.zero);

            Assert.That(selector.CurrentUnit.IsPresent, Is.True);

            selector.Unselect();

            Assert.That(selector.CurrentUnit.IsPresent, Is.False);
        }

        [Test]
        public void Should_select_when_object_returned_by_raycast_is_selectable()
        {
            var raycast = new RaycastHandlerMockBuilder()
                .WithReturnedObject<ISelectable>()
                .Build();

            var selector = new RaycastSelector(raycast);

            var selected = selector.Select(Vector3.zero);

            Assert.That(selected.IsPresent, Is.True);
            Assert.That(selector.CurrentUnit.IsPresent, Is.True);
        }
    }
}