using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D
{
    public class IdleCharacterState : BaseCharacterState2D
    {
        protected readonly Player player;
        protected readonly Rigidbody2D rigidbody;

        public IdleCharacterState(Player player)
        {
            this.player = player;
            rigidbody = player.GetComponent<Rigidbody2D>();
        }

        public override void EnterState()
        {
            player.GetComponent<Animator>().Play("idle");
        }

        public override void ExitState()
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public override void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                player.TransitionToState(player.jumpingState);
            }

            if(Input.GetAxisRaw("Horizontal") != 0f)
            {
                player.TransitionToState(player.walkingState);
            }

            if(Input.GetMouseButtonDown(0))
            {
                player.TransitionToState(player.attackingState);
            }
        }

        public override void FixedUpdate()
        {
            if(player.IsOnGround)
            {
                rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                rigidbody.velocity = new Vector2(0f, 0f);
            }
            else
            {
                player.TransitionToState(player.jumpingState);
            }
        }
    }
}