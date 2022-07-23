using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D
{
    public class AttackCharacterState : BaseCharacterState2D
    {
        private const string attckAnimation = "attack";

        private readonly Player player;
        private readonly Animator animator;
        private readonly Rigidbody2D rigidbody;

        public AttackCharacterState(Player player)
        {
            this.player = player;
            animator = player.GetComponent<Animator>();
            rigidbody = player.GetComponent<Rigidbody2D>();
        }

        public override void EnterState()
        {
            animator.Play(attckAnimation);
        }

        public override void FixedUpdate()
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }

        public override void Update()
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.99f) return;

            if(player.IsOnGround)
            {
                player.TransitionToState(player.idleState);
            }
            else
            {
                player.TransitionToState(player.jumpingState);
            }
        }
    }
}