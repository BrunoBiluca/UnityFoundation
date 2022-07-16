using NUnit.Framework;
using System;
using UnityFoundation.Code;

namespace UnityFoundation.DiceSystem.Tests
{
    public class DiceRollTests
    {
        [Test]
        public void ShouldThrowExceptionWhenDiceHasNoSides()
        {
            var dice = new DiceRoll(
                new DummyRandomGenerator(new int[] { 0 })
            );
            var diceIndex = dice.AddDice(new NumberedDice());

            Assert.Throws<InvalidOperationException>(() => dice.Roll(diceIndex));
        }

        [Test]
        public void ShouldReturnSideWhenOneSideIsSetup()
        {
            var dice = new NumberedDice();
            dice.AddSide(1);

            var diceRoll = new DiceRoll(new DummyRandomGenerator(new int[] { 0 }));
            var diceIndex = diceRoll.AddDice(dice);

            Assert.AreEqual(0, diceRoll.Roll(diceIndex));
        }

        [Test]
        public void ShouldReturnSideWhenMultipleSidesAreSetup()
        {
            var dice = new Dice<int>();
            dice.AddSide(1);
            dice.AddSide(2);
            dice.AddSide(3);

            var diceRoll = new DiceRoll(new DummyRandomGenerator(new int[] { 1 }));
            var diceIndex = diceRoll.AddDice(dice);

            var rolledSide = diceRoll.Roll(diceIndex);
            Assert.AreEqual(2, dice.GetSide(rolledSide).GetValue());
        }

        [Test]
        public void ShouldReturnMultipleSidesWhenMultipleDicesAreRolled()
        {
            var dice = new Dice<int>();
            dice.AddSide(1);
            dice.AddSide(2);
            dice.AddSide(3);

            var diceRoll = new DiceRoll(new DummyRandomGenerator(new int[] { 1, 2 }));
            diceRoll.AddDice(dice);
            diceRoll.AddDice(dice);

            var rolledSide = diceRoll.RollAll();

            Assert.AreEqual(2, dice.GetSide(rolledSide[0]).GetValue());
            Assert.AreEqual(3, dice.GetSide(rolledSide[1]).GetValue());
        }

        [Test]
        public void ShouldReturnMultipleSidesWhenMultipleDicesOfDifferentTypesAreRolled()
        {
            var diceRoll = new DiceRoll(new DummyRandomGenerator(new int[] { 1, 2 }));

            var dice = new Dice<int>();
            dice.AddSide(1);
            dice.AddSide(2);
            dice.AddSide(3);
            diceRoll.AddDice(dice);

            var diceString = new Dice<string>();
            diceString.AddSide("test_1");
            diceString.AddSide("test_2");
            diceString.AddSide("test_3");
            diceRoll.AddDice(diceString);

            var rolledSide = diceRoll.RollAll();

            Assert.AreEqual(2, dice.GetSide(rolledSide[0]).GetValue());
            Assert.AreEqual("test_3", diceString.GetSide(rolledSide[1]).GetValue());
        }
    }
}