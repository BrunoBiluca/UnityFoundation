using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.DiceSystem
{
    public class InitialPositionThrow : IThrowDiceHandler
    {
        private readonly Vector3 initialPosition;

        public InitialPositionThrow(Vector3 initialPosition)
        {
            this.initialPosition = initialPosition;
        }

        public void Handle(ThrowDicesEvent throwEvent)
        {
            foreach(IDiceMono dice in throwEvent.Dices)
                dice.GetTransform().Position = initialPosition;

            throwEvent.StartChecking();
        }
    }
}