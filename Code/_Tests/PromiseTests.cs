using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityFoundation.Code.Promise;

namespace UnityFoundation.Code.Promise.Tests
{
    public class TestPromise
    {
        public Promise Execute()
        {
            var promise = Promise.Create();
            promise.Resolve();
            return promise.Promise();
        }

        public Promise ExecuteFailed()
        {
            var promise = Promise.Create();
            promise.Reject();
            return promise.Promise();
        }
    }

    public class PromiseTests : MonoBehaviour
    {
        [Test]
        public void Should_resolve_if_action_is_empty()
        {
            var result = new TestPromise().Execute();

            result
                .Then(() => Assert.Pass())
                .Catch(() => Assert.Fail());
        }

        [Test]
        public void Should_catch__when_actin_is_rejected()
        {
            var result = new TestPromise().ExecuteFailed();

            result
                .Then(() => Assert.Fail())
                .Catch(() => Assert.Pass());
        }
    }
}