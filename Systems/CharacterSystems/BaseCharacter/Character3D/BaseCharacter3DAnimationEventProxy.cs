using System;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.Character3D.Scripts.Helpers
{
    /// <summary>
    /// Helper MonoBehaviour class. Act as a proxy when the Animator component 
    /// is in a child object of the Character 3D component. 
    /// Pass through the animation event triggered by the animation of the child object.
    /// </summary>
    [Obsolete("Should use the AnimationEventProxy class in UnityFoundation.Code.Extensions")]
    public class BaseCharacter3DAnimationEventProxy : MonoBehaviour
    {
        [SerializeField] private BaseCharacter3D baseCharacter;

        private void Start()
        {
            if(baseCharacter == null)
                baseCharacter = GetComponentInParent<BaseCharacter3D>();
        }

        public void TriggerAnimationEvent(string name)
        {
            baseCharacter.TriggerAnimationEvent(name);
        }
    }
}