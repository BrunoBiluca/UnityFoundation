using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Tests
{
    public class BinaryDecisionTreeTests
    {
        [Test]
        public void Should_execute_first_decision_given_decision_is_true()
        {
            var firstDecision = new BinaryDecision(() => true);
            var decisionTree = new BinaryDecisionTree(firstDecision);

            decisionTree.Execute();

            Assert.That(firstDecision.WasSuccessful, Is.True);
        }

        [Test]
        public void Should_execute_successful_branch_given_first_decision_is_true()
        {
            var firstDecision = new BinaryDecision(() => true);
            var successDecision = new BinaryDecision(() => true);
            var failedDecision = new BinaryDecision(() => true);

            firstDecision.SetNext(successDecision).SetFailed(failedDecision);

            var decisionTree = new BinaryDecisionTree(firstDecision);

            decisionTree.Execute();

            Assert.That(firstDecision.WasSuccessful, Is.True);
            Assert.That(successDecision.WasSuccessful, Is.True);
            Assert.That(failedDecision.WasSuccessful, Is.False);
        }

        [Test]
        public void Should_execute_failed_branch_given_first_decision_is_false()
        {
            var firstDecision = new BinaryDecision(() => false);
            var successDecision = new BinaryDecision(() => true);
            var failedDecision = new BinaryDecision(() => true);

            firstDecision.SetNext(successDecision).SetFailed(failedDecision);

            var decisionTree = new BinaryDecisionTree(firstDecision);

            decisionTree.Execute();

            Assert.That(firstDecision.WasSuccessful, Is.False);
            Assert.That(successDecision.WasSuccessful, Is.False);
            Assert.That(failedDecision.WasSuccessful, Is.True);
        }

        
        [Test]
        public void Should_execute_until_final_decision_given_tree_has_it()
        {
            var firstDecision = new BinaryDecision(() => true);
            var finalDecision = new Decision(() => true);
            var successNextDecision = new BinaryDecision(() => true);
            var failedDecision = new BinaryDecision(() => true);

            firstDecision.SetFinal(finalDecision).SetFailed(failedDecision);

            var decisionTree = new BinaryDecisionTree(firstDecision);

            decisionTree.Execute();

            Assert.That(firstDecision.WasSuccessful, Is.True);
            Assert.That(finalDecision.WasSuccessful, Is.True);
            Assert.That(successNextDecision.WasSuccessful, Is.False);
            Assert.That(failedDecision.WasSuccessful, Is.False);
        }
    }
}
