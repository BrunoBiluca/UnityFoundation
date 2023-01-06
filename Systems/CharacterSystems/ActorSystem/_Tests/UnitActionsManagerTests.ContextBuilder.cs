using Moq;
using UnityFoundation.CharacterSystem.ActorSystem;
using UnityFoundation.ResourceManagement;
using UnityFoundation.TestUtility;

namespace UnityFoundation.CharacterSystem.ActorSystem.Tests
{
    public partial class UnitActionsManagerTests
    {
        public class ContextBuilder
        {
            public Mock<IAPActionIntent> MockIntent { get; private set; }

            public EventTest CantExecuteAction { get; private set; }

            IResourceManager actionPoints;
            private bool isActionSet;
            private int actionPointsCost = 1;

            public ContextBuilder()
            {
                isActionSet = false;
                MockIntent = new Mock<IAPActionIntent>();
                var action = new Mock<IAction>();
                action.Setup(a => a.Execute()).Raises(a => a.OnFinishAction += null);
                MockIntent.Setup(i => i.Create()).Returns(action.Object);
                actionPoints = new FiniteResourceManager(0, true);
            }

            public ContextBuilder WithActionSet(int actionPointsCost = 1)
            {
                isActionSet = true;
                this.actionPointsCost = actionPointsCost;
                return this;
            }

            public ContextBuilder WithImmediateAction()
            {
                isActionSet = true;
                MockIntent.SetupGet(a => a.ExecuteImmediatly).Returns(true);
                return this;
            }

            public ContextBuilder WithInitialActionPoints(int amount)
            {
                actionPoints = new FiniteResourceManager((uint)amount, true);
                return this;
            }

            public APActor Build()
            {
                var actor = new APActor(actionPoints);

                if(isActionSet)
                {
                    MockIntent.SetupGet(i => i.ActionPointsCost).Returns(actionPointsCost);
                    actor.Set(MockIntent.Object);
                }

                CantExecuteAction = EventTest.Create(actor, nameof(actor.OnCantExecuteAction));

                return actor;
            }
        }
    }
}