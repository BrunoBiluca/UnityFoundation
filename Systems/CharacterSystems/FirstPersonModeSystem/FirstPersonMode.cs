using Assets.UnityFoundation.Systems.Character3D.Scripts;
using System;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;
using UnityFoundation.Physics3D.CheckGround;
using UnityFoundation.Tools.TimeUtils;
using Zenject;

namespace UnityFoundation.FirstPersonModeSystem
{
    public class FirstPersonMode : BaseCharacter3D
    {
        public FirstPersonModeSettings Settings { get; private set; }
        public IFirstPersonInputs Inputs { get; private set; }
        public ICheckGroundHandler CheckGroundHandler { get; private set; }
        public FirstPersonAnimationController AnimController { get; private set; }
        public IAudioSource AudioSource { get; private set; }
        public IRigidbody Rigidbody { get; set; }
        public Transform WeaponShootPoint;

        public IdlePlayerState IdlePlayerState;
        public AimPlayerState AimState;

        /// <summary>
        /// Returns as a parameter the shot hit point
        /// </summary>
        public Action<Vector3> OnShotHit { get; set; }

        private Camera mainCamera;
        private Timer walkStepTimer;

        // TODO: remover esse inject, deve ter uma forma de chamar esse método do instaler sem ter que definir essa anotação, já que esse código irá para UnityFoundation
        [Inject]
        public FirstPersonMode Setup(
            FirstPersonModeSettings settings,
            IFirstPersonInputs inputs,
            ICheckGroundHandler checkGroundHandler,
            FirstPersonAnimationController animController,
            IAudioSource audioSource,
            Camera camera
        )
        {
            Settings = settings;
            Inputs = inputs;
            CheckGroundHandler = checkGroundHandler;
            AnimController = animController;
            AudioSource = audioSource;
            mainCamera = camera;

            IdlePlayerState = new IdlePlayerState(this);
            AimState = new AimPlayerState(this);

            CheckGroundHandler.OnLanded += OnLandedHandler;

            walkStepTimer = (Timer)new Timer(0.4f, UpdateWalkingStepClip).Loop();
            return this;
        }

        private void UpdateWalkingStepClip()
        {
            if(Settings.WalkingStepsSFX == null) return;

            var clipIdx = UnityEngine.Random.Range(0, Settings.WalkingStepsSFX.Count - 1);
            AudioSource.Play(Settings.WalkingStepsSFX[clipIdx]);
            AudioSource.Loop = true;
        }

        private void OnLandedHandler()
        {
            AudioSource.PlayOneShot(Settings.LandAudioClip);
        }

        protected override void OnStart()
        {
            Inputs.Enable();

            TransitionToState(IdlePlayerState);
        }

        protected override void OnUpdate()
        {
            CheckGroundHandler.CheckGround();

            if(TryAim())
                return;

            TryJump();
        }

        private bool TryAim()
        {
            if(!Inputs.Aim) return false;

            if(CurrentState != AimState)
                TransitionToState(AimState);
            else
                AnimController.ToggleAim();
            return true;
        }

        public void ShootHit(Vector3 point)
        {
            OnShotHit?.Invoke(point);
        }

        public void Rotate()
        {
            transform.rotation = Quaternion.Euler(
                mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 0f
            );
        }

        public void Move()
        {
            var targetDirection = new Vector3(Inputs.Move.x, 0f, Inputs.Move.y).normalized;

            AnimController.Walking(targetDirection.magnitude > 0f);

            UpdateWalkStepAudioClips(targetDirection);

            var newPos = transform.forward * targetDirection.z
                + transform.right * targetDirection.x;
            transform.position += Settings.MoveSpeed * Time.deltaTime * newPos;
        }

        private void UpdateWalkStepAudioClips(Vector3 targetDirection)
        {
            if(targetDirection.magnitude == 0f)
            {
                AudioSource.Stop();
                walkStepTimer.Stop();
                return;
            }

            if(!walkStepTimer.IsRunning)
                walkStepTimer.Start();
        }

        public void TryJump()
        {
            if(!CheckGroundHandler.IsGrounded) return;

            if(!Inputs.Jump) return;

            CheckGroundHandler.Disable(new UnityTimer().SetAmount(1f));

            AudioSource.PlayOneShot(Settings.JumpAudioClip);
            Rigidbody.AddForce(Vector3.up * Settings.JumpForce, ForceMode.Impulse);
        }
    }
}