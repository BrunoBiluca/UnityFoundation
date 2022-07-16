using NUnit.Framework;
using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.TestUtility;

namespace UnityFoundation.DiceSystem.Tests
{
    public partial class DicePhysicallyTests
    {
        DiceRollManager diceRollManager;
        DiceSideChecker floor;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            diceRollManager = Builder.DiceRollManager();
            floor = Builder.DiceChecker();
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            Object.DestroyImmediate(diceRollManager.gameObject);
            Object.DestroyImmediate(floor.gameObject);
        }

        [Test]
        public void ShouldNotCheckDiceSideWhenDiceIsNotThrown()
        {
            var dice = Builder.OneSidedNumberedDice(1);

            var diceHandler = new StringfyDiceSideHandler();

            floor.OnCollisionStay(CollisionFactory.Create(dice));

            Assert.IsNull(diceHandler.DiceSideValue);

            Object.DestroyImmediate(dice.gameObject);
        }

        [Test]
        public void ShouldCheckDiceSideWhenSingleDiceIsThrown()
        {
            const int expected = 1;

            var dice = Builder.OneSidedNumberedDice(expected);
            var diceHandler = new StringfyDiceSideHandler();

            var throwEvent = diceRollManager.ThrowDice(
                new InitialPositionThrow(Vector3.zero),
                dice
            );

            throwEvent.OnDiceEvaluate += diceHandler.Handle;

            floor.OnCollisionStay(CollisionFactory.Create(dice));

            Assert.AreEqual($"{expected}", diceHandler.DiceSideValue);

            Object.DestroyImmediate(dice.gameObject);
        }

        [Test]
        public void ShouldCheckDiceSideWhenSingleDiceIsThrownRandomly()
        {
            const int expected = 1;

            var dice = Builder.OneSidedNumberedDice(expected);
            var diceHandler = new StringfyDiceSideHandler();

            var asyncProcessor = new DummyAsyncProcessor();
            var throwHandler = new RandomThrow(asyncProcessor, new DummyRandomGenerator(400, 0));
            var throwEvent = diceRollManager.ThrowDice(
                throwHandler,
                dice
            );

            asyncProcessor.Update(throwHandler.StartCheckingThrowEventTime);

            throwEvent.OnDiceEvaluate += diceHandler.Handle;
            floor.OnCollisionStay(CollisionFactory.Create(dice));

            Assert.AreEqual($"{expected}", diceHandler.DiceSideValue);

            Object.DestroyImmediate(dice.gameObject);
        }


        [Test]
        public void ShouldCheckDiceSideWhenGenericDiceIsThrown()
        {
            var value = new MultiplierValue() { Amount = 3, Multiplier = 1.5f };
            var dice = Builder.OneSidedCustomDice(value);
            var diceHandler = new StringfyDiceSideHandler();

            var throwEvent = diceRollManager.ThrowDice(
                new InitialPositionThrow(Vector3.zero),
                dice
            );

            throwEvent.OnDiceEvaluate += diceHandler.Handle;

            floor.OnCollisionStay(CollisionFactory.Create(dice));

            Assert.AreEqual(value.ToString(), diceHandler.DiceSideValue);

            Object.DestroyImmediate(dice.gameObject);
        }

        [Test]
        public void ShouldCheckMultipleDiceSidesWhenMultipleDicesAreThrown()
        {
            var numberedDiceValue = 0;
            var dice1 = Builder.OneSidedNumberedDice(0);

            var customValue = new MultiplierValue() { Amount = 3, Multiplier = 1.5f };
            var dice2 = Builder.OneSidedCustomDice(customValue);
            var diceHandler = new StringfyDiceSideHandler();

            var throwEvent = diceRollManager.ThrowDice(
                new InitialPositionThrow(Vector3.zero),
                dice1,
                dice2
            );

            throwEvent.OnDiceEvaluate += diceHandler.Handle;

            floor.OnCollisionStay(CollisionFactory.Create(dice2));
            Assert.AreEqual(customValue.ToString(), diceHandler.DiceSideValue);

            floor.OnCollisionStay(CollisionFactory.Create(dice1));
            Assert.AreEqual(numberedDiceValue.ToString(), diceHandler.DiceSideValue);

            Object.DestroyImmediate(dice1.gameObject);
            Object.DestroyImmediate(dice2.gameObject);
        }
    }
}