using System;
using UnityEngine;

namespace UnityFoundation.ThirdPersonCharacter
{
    public partial class ThirdPersonController
    {
        [Serializable]
        public class GroundedSettings
        {
            [Tooltip("Useful for rough ground")]
            public float GroundedOffset = -0.14f;

            [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
            public float GroundedRadius = 0.28f;

            [Tooltip("What layers the character uses as ground")]
            public LayerMask GroundLayers;
        }
    }
}