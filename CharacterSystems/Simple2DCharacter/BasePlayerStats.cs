using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D
{
    [CreateAssetMenu(
        fileName = "new_player_stats", 
        menuName = "Character2D/PlayerStats"
    )]
    public class BasePlayerStats : ScriptableObject
    {
        [SerializeField] private float walkSpeed;
        public float WalkSpeed => walkSpeed;

        [SerializeField] private float jumpForce;
        public float JumpForce => jumpForce;

    }
}