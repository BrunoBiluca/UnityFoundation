using UnityEngine;
using UnityFoundation.BaseCharacter;

namespace Assets.UnityFoundation.Code.Character2D
{
    public abstract class BaseCharacter2D : BaseCharacter<BaseCharacterState2D>
    {
        protected virtual void PosOnCollisionEnter2D(Collision2D collision) { }
        protected virtual void PosOnTriggerStay(Collider2D collision) { }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CurrentState?.OnCollisionEnter(collision);
            PosOnCollisionEnter2D(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            CurrentState.OnTriggerStay(collision);
            PosOnTriggerStay(collision);
        }
    }
}