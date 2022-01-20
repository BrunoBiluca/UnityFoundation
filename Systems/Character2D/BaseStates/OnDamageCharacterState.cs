using Assets.UnityFoundation.Code.Character2D;
using Assets.UnityFoundation.Code.TimeUtils;
using Assets.UnityFoundation.TimeUtils;
using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D
{
    public class OnDamageCharacterState : BaseCharacterState2D
    {
        private readonly Rigidbody2D rigidbody;
        private readonly SpriteRenderer spriteRenderer;
        private readonly Timer changeMaterialTimer;

        private bool tookDamage;

        public OnDamageCharacterState(Player player)
        {
            rigidbody = player.GetComponent<Rigidbody2D>();
            spriteRenderer = player.GetComponent<SpriteRenderer>();

            changeMaterialTimer = new Timer(
                .2f,
                () => player.TransitionToState(player.idleState)
            )
                .SetName("Player OnDamageState")
                .RunOnce();
        }

        public override void EnterState()
        {
            tookDamage = true;
            changeMaterialTimer.Start();
            rigidbody.velocity = Vector2.zero;
        }

        public override void FixedUpdate()
        {
            if(tookDamage)
            {
                var direction = spriteRenderer.flipX ? Vector2.right.x : Vector2.left.x;
                rigidbody.AddForce(new Vector2(direction * 60f, Vector2.up.y * 20));
                tookDamage = false;
            }
        }
    }
}