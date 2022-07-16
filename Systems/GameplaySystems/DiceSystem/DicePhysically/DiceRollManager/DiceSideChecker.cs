using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.DiceSystem
{
    public class DiceSideChecker : Singleton<DiceSideChecker>
    {
        private ThrowDicesEvent throwDices;

        public void Start()
        {
            DiceRollManager.Instance.OnDiceThrow += (throwDices) => {
                this.throwDices = throwDices;
            };
        }

        public void OnCollisionStay(Collision collision)
        {
            CheckCollision(collision);
        }

        private void CheckCollision(Collision collision)
        {
            if(throwDices == null) return;
            if(!throwDices.IsChecking) return;

            if(!TryGetDice(collision, out IDiceMono dice)) return;

            var side = dice.CheckSelectedSide();

            throwDices.Evaluate(dice, side);
        }

        private bool TryGetDice(Collision collision, out IDiceMono dice)
        {
            if(!collision.gameObject.TryGetComponent(out dice))
                return false;

            if(!dice.GetRigidbody().Velocity.magnitude.NearlyEqual(0, 0.001f))
                return false;

            if(!dice.GetRigidbody().AngularVelocity.magnitude.NearlyEqual(0, 0.001f))
                return false;

            return true;
        }
    }
}