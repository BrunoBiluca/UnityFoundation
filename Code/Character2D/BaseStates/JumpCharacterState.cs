using UnityEngine;

public class JumpCharacterState : BaseCharacterState
{
    private readonly Player player;
    private readonly Rigidbody2D rigidbody;
    private readonly Animator animator;
    private readonly SpriteRenderer spriteRenderer;
    private bool checkFalling;
    private float inputX;

    public JumpCharacterState(Player player)
    {
        this.player = player;
        rigidbody = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();

        checkFalling = true;
    }

    public override void EnterState()
    {
        animator.Play("jump");
    }

    public override void FixedUpdate()
    {
        if(player.IsOnGround)
        {
            const float jumpForce = 3000f;
            rigidbody.AddForce(Vector2.up * jumpForce);
            player.IsOnGround = false;
        }

        const float speed = 3000f;
        rigidbody.velocity = new Vector2(speed * inputX * Time.deltaTime, rigidbody.velocity.y);
    }

    public override void OnCollisionEnter(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("ground")) return;

        checkFalling = true;

        player.TransitionToState(player.idleState);
    }

    public override void Update()
    {
        if(checkFalling && rigidbody.velocity.y < 0)
        {
            animator.Play("falling");
            checkFalling = false;
        }

        inputX = Input.GetAxisRaw("Horizontal");
        if(inputX > 0) spriteRenderer.flipX = false;
        else if(inputX < 0) spriteRenderer.flipX = true;
    }
}
