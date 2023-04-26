using Assets.UnityFoundation.Code.Character2D.BaseStates;
using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D
{
    public class Player : BaseCharacter2D
    {
        [SerializeField] private BasePlayerStats stats;
        public BasePlayerStats Stats => stats;

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