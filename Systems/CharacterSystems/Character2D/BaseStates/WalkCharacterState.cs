using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D.BaseStates
{
    public class WalkCharacterState : BaseCharacterState2D
    {
        private const string startWalkAnimation = "start_walk";
        private const string walkAnimation = "walk";

        protected readonly Player player;
        protected readonly Animator animator;
        protected readonly Rigidbody2D rigidbody;
        protected readonly SpriteRenderer spriteRenderer;

        protected float inputX;

        public WalkCharacterState(Player player)
        {
            this.player = player;
            animator = player.GetComponent<Animator>();
            rigidbody = player.GetComponent<Rigidbody2D>();
            spriteRenderer = player.GetComponent<SpriteRenderer>();
        }

        public override void EnterState()
        {
            animator.Play(startWalkAnimation);
        }

        public override void FixedUpdate()
        {
            var velocityX = player.Stats.WalkSpeed * inputX * Time.deltaTime;

            rigidbody.velocity = new Vector2(velocityX, rigidbody.velocity.y);
        }

        public override void Update()
        {
            inputX = Input.GetAxisRaw("Horizontal");

            if(inputX == 0f)
            {
                player.TransitionToState(player.idleState);
                return;
            }

            if(inputX > 0) spriteRenderer.flipX = false;
            else if(inputX < 0) spriteRenderer.flipX = true;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                player.TransitionToState(player.jumpingState);
            }

            if(Input.GetMouseButton(0))
            {
                player.TransitionToState(player.attackingState);
            }
        }
    }
}