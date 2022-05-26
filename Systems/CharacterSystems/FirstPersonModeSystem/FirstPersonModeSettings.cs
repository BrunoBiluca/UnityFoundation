using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.FirstPersonModeSystem
{
    [CreateAssetMenu(
        fileName = "new_first_person_settings",
        menuName = "First Person Mode/Settings"
    )]
    public class FirstPersonModeSettings : ScriptableObject
    {
        [field: Tooltip("Move speed of the character in m/s")]
        [field: SerializeField] public float MoveSpeed { get; set; } = 2.0f;

        [field: Space(10)]
        [field: Tooltip("The height the player can jump")]
        [field: SerializeField] public float JumpHeight { get; set; } = 1.2f;

        [field: Space(10)]
        [field: Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        [field: SerializeField] public float JumpTimeout { get; set; } = 0.50f;

        [field: SerializeField] public float JumpForce { get; set; } = 7f;

        [field: SerializeField] public AudioClip FireSFX { get; set; }
        [field: SerializeField] public AudioClip FireMissSFX { get; set; }

        [field: SerializeField] public List<AudioClip> WalkingStepsSFX { get; set; }

        [field: SerializeField] public AudioClip JumpAudioClip { get; set; }

        [field: SerializeField] public AudioClip LandAudioClip { get; set; }
    }
}