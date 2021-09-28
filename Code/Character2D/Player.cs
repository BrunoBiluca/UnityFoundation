namespace Assets.UnityFoundation.Code.Character2D
{
    public class Player : BaseCharacter
    {
        public bool IsOnGround { get; set; }

        public IdleCharacterState idleState;
        public WalkCharacterState walkingState;
        public AttackCharacterState attackingState;
        public JumpCharacterState jumpingState;
        public OnDamageCharacterState onDamageState;

        protected override void SetCharacterStates()
        {
            idleState = new IdleCharacterState(this);
            jumpingState = new JumpCharacterState(this);
            walkingState = new WalkCharacterState(this);
            onDamageState = new OnDamageCharacterState(this);
            attackingState = new AttackCharacterState(this);

            TransitionToState(idleState);
        }
    }
}