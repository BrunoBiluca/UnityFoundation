using UnityEngine;
using UnityFoundation.BaseCharacter;

namespace Assets.UnityFoundation.Systems.Character3D.Scripts
{
    public class BaseCharacterState3D : BaseCharacterState
    {
        public virtual void OnCollisionEnter(Collision collision) { }
        public virtual void OnCollisionStay(Collision collision) { }
        public virtual void OnTriggerStay(Collider collision) { }
    }
}