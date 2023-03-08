using System;
using UnityEngine;

namespace UnityFoundation.ThirdPersonCharacter
{
    public partial class ThirdPersonController
    {
        [Serializable]
        public class PlayerSettings
        {
            [Header("Player")]

            [Tooltip("Move speed of the character in m/s")]
            public float MoveSpeed = 2.0f;

            [Tooltip("Sprint speed of the character in m/s")]
            public float SprintSpeed = 5.335f;

            [Tooltip("How fast the character turns to face movement direction")]
            [Range(0.0f, 0.3f)]
            public float RotationSmoothTime = 0.12f;

            [Tooltip("Acceleration and deceleration")]
            public float SpeedChangeRate = 10.0f;

            [Space(10)]
            [Tooltip("The height the player can jump")]
            public float JumpHeight = 1.2f;

            [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
            public float Gravity = -15.0f;

            [Space(10)]
            [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
            public float JumpTimeout = 0.50f;

            [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
            public float FallTimeout = 0.15f;
        }
    }
}