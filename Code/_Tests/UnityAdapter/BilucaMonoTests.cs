using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code.Tests
{
    public class BilucaMonoTests
    {

        public class TestBilucaMono : BilucaMono { }

        [Test]
        public void Should_be_the_same_instance_when_in_same_object()
        {
            var gameObject = new GameObject("obj");

            var mono1 = gameObject.AddComponent<TestBilucaMono>();
            mono1.Awake();
            var mono2 = gameObject.AddComponent<TestBilucaMono>();
            mono2.Awake();

            Assert.That(ReferenceEquals(mono1, mono2), Is.False);
            Assert.That(ReferenceEquals(mono1.Obj, mono2.Obj), Is.True);

            Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void Should_not_be_the_same_instance_when_are_in_different_objects()
        {
            var gameObject1 = new GameObject("obj_1");
            var mono1 = gameObject1.AddComponent<TestBilucaMono>();
            mono1.Awake();

            var gameObject2 = new GameObject("obj_2");
            var mono2 = gameObject2.AddComponent<TestBilucaMono>();
            mono2.Awake();

            Assert.That(ReferenceEquals(mono1, mono2), Is.False);
            Assert.That(ReferenceEquals(mono1.Obj, mono2.Obj), Is.False);

            Object.DestroyImmediate(gameObject1);
            Object.DestroyImmediate(gameObject2);
        }
    }
}
