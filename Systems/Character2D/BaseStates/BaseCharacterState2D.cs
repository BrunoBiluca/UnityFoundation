using UnityEngine;
using UnityFoundation.Code.Characters;

namespace Assets.UnityFoundation.Code.Character2D
{
    public abstract class BaseCharacterState2D : BaseCharacterState
    {
        public virtual void OnCollisionEnter(Collision2D collision) { }
        public virtual void OnCollisionStay(Collision2D collision) { }
        public virtual void OnTriggerStay(Collider2D collision) { }
    }
}