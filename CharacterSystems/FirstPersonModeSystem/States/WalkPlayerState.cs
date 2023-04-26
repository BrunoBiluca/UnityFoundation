using UnityEngine;
using Assets.UnityFoundation.Systems.Character3D.Scripts;
using UnityFoundation.Code.Timer;

namespace UnityFoundation.FirstPersonModeSystem
{
    public class WalkPlayerState : BaseCharacterState3D
    {
        private readonly FirstPersonMode controller;
        private readonly Timer walkStepTimer;

        public WalkPlayerState(FirstPersonMode controller)
        {
            this.controller = controller;

            // TODO: Passar essa instanciação do timer para um factory do DI
            UpdateWalkingStepClip();
            walkStepTimer = (Timer)new Timer(0.4f, UpdateWalkingStepClip).Loop();
        }

        private void UpdateWalkingStepClip()
        {
            Debug.Log("UpdateWalkingStepClip");
            if(controller.Settings.WalkingStepsSFX == null) return;

            var clipIdx = Random.Range(0, controller.Settings.WalkingStepsSFX.Count - 1);
            controller.AudioSource.Play(controller.Settings.WalkingStepsSFX[clipIdx]);
            controller.AudioSource.Loop = true;
        }

        public override void EnterState()
        {
            controller.AnimController.Walking(true);

            walkStepTimer.Start();
        }

        public override void Update()
        {
            controller.Rotate();

            if(controller.Inputs.Move == Vector2.zero)
            {
                controller.TransitionToState(controller.IdlePlayerState);
                return;
            }

            controller.Move();
        }

        public override void ExitState()
        {
            walkStepTimer.Close();
            controller.AudioSource.Stop();
        }
    }
}