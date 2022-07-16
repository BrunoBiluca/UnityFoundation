using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.DiceSystem
{
    public class RandomThrow : IThrowDiceHandler
    {
        private readonly IAsyncProcessor asyncProcessor;
        private readonly IRandomGenerator random;

        public float StartCheckingThrowEventTime => 0.1f;

        public RandomThrow(
            IAsyncProcessor asyncProcessor,
            IRandomGenerator randomGenerator
        )
        {
            this.asyncProcessor = asyncProcessor;
            random = randomGenerator;
        }

        public void Handle(ThrowDicesEvent throwDices)
        {
            foreach(var dice in throwDices.Dices)
            {
                var forceValue = random.Range(400, 800);
                var directionX = random.Range(0, 500);
                var directionY = random.Range(0, 500);
                var directionZ = random.Range(0, 500);

                var diceRb = dice.GetRigidbody();
                diceRb.AddForce(Vector3.up * forceValue);
                diceRb.AddTorque(directionX, directionY, directionZ);
            }

            asyncProcessor.ProcessAsync(
                () => throwDices.StartChecking(),
                StartCheckingThrowEventTime
            );
        }
    }
}