using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Tests
{
    public class ContextDecisionTreeTests
    {
        [Test]
        public void Should_throw_exceptions_when_tree_is_not_fully_setup()
        {
            var tree = new DecisionTree<int>();

            Assert.Throws<ArgumentNullException>(() => tree.Evaluate());
        }

        [Test]
        public void Should_evaluate_success_path()
        {
            var initialContext = true;
            var root = new BinaryDecision<bool>(v => v);
            var success = new BinaryDecision<bool>(_ => true);
            var failed = new BinaryDecision<bool>(_ => true);

            var tree = new DecisionTree<bool>(initialContext) {
                Root = root.SetNext(success).SetFailed(failed)
            };

            tree.Evaluate();

            Assert.That(root.WasSuccessful, Is.True);
            Assert.That(success.WasSuccessful, Is.True);
            Assert.That(failed.WasSuccessful, Is.False);
        }

        [Test]
        public void Should_evaluate_failed_path()
        {
            var initialContext = false;
            var root = new BinaryDecision<bool>(v => v);
            var success = new BinaryDecision<bool>(_ => true);
            var failed = new BinaryDecision<bool>(_ => true);

            var tree = new DecisionTree<bool>(initialContext) {
                Root = root.SetNext(success).SetFailed(failed)
            };

            tree.Evaluate();

            Assert.That(root.WasSuccessful, Is.False);
            Assert.That(success.WasSuccessful, Is.False);
            Assert.That(failed.WasSuccessful, Is.True);
        }
    }
}
