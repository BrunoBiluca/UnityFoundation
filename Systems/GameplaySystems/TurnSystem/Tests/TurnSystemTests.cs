using NUnit.Framework;

namespace UnityFoundation.TurnSystem.Tests
{
    public class TurnSystemTests
    {

        [Test]
        public void Should_update_turn_count_when_turn_changed()
        {
            var turnSystem = new TurnSystem();

            turnSystem.EndPlayerTurn();

            Assert.That(turnSystem.CurrentTurn, Is.EqualTo(2));
        }

        [Test]
        public void Should_execute_action_when_turn_changed()
        {
            var turnSystem = new TurnSystem();

            var wasExecuted = false;
            turnSystem.OnPlayerTurnEnded += () => wasExecuted = true;

            turnSystem.EndPlayerTurn();

            Assert.That(wasExecuted, Is.True);
        }

    }
}