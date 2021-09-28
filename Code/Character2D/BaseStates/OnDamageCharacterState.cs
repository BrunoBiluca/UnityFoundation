using Assets.UnityFoundation.Code.Character2D;
using Assets.UnityFoundation.TimeUtils;
using UnityEngine;

public class OnDamageCharacterState : BaseCharacterState
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
            "Player OnDamageState",
            .2f,
            () => {
                player.TransitionToState(player.idleState);
            },
            runOnce: true
        );
    }
    
    public override void EnterState()
    {
        tookDamage = true;
        changeMaterialTimer.Restart();
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
