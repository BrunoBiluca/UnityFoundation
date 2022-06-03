using UnityEngine;
using UnityFoundation.BaseCharacter;

namespace Assets.UnityFoundation.Systems.Character3D.Scripts
{
    public abstract class BaseCharacter3D : BaseCharacter<BaseCharacterState3D>
    {
        protected virtual void PosOnCollisionEnter(Collision collision) { }
        protected virtual void PosOnTriggerStay(Collider collision) { }

        private void OnCollisionEnter(Collision collision)
        {
            CurrentState?.OnCollisionEnter(collision);
            PosOnCollisionEnter(collision);
        }

        private void OnTriggerStay(Collider collision)
        {
            CurrentState.OnTriggerStay(collision);
            PosOnTriggerStay(collision);
        }
    }
}