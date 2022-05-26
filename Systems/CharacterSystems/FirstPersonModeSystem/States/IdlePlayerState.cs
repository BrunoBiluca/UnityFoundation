using Assets.UnityFoundation.Systems.Character3D.Scripts;
using System.Numerics;

namespace UnityFoundation.FirstPersonModeSystem
{
    public class IdlePlayerState : BaseCharacterState3D
    {
        private readonly FirstPersonMode controller;

        public IdlePlayerState(FirstPersonMode controller)
        {
            this.controller = controller;
        }

        public override void EnterState()
        {
            controller.AnimController.Walking(false);
        }

        public override void Update()
        {
            controller.Move();
            controller.Rotate();
        }
    }
}