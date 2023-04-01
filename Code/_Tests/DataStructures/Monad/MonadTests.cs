using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Tests
{
    public class MonadTests
    {
        [Test]
        public void Should_map_values()
        {
            var monad = new Monad<int>(41);
            var newValue = monad.Map(v => v + 1);
            Assert.That(newValue.Returns(), Is.EqualTo(42));
        }

        [Test]
        public void Should_chain_functions()
        {
            var monad = new Monad<int>(41);
            var newValue = monad
                .Map(v => v + 1)
                .Chain(v => $"{v} + 1");
            Assert.That(newValue.Returns(), Is.EqualTo("42 + 1"));
        }

        [Test]
        public void Should_create_monad_by_static_method()
        {
            var result = Monad<int>
                .Init(41)
                .Map(v => v + 1)
                .Chain(v => $"{v} + 1")
                .Returns();

            Assert.That(result, Is.EqualTo("42 + 1"));
        }
    }
}
