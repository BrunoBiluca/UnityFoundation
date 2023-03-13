using UnityEngine;
using UnityEngine.InputSystem;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.ThirdPersonCharacter
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public partial class ThirdPersonController : MonoBehaviour
    {
        [SerializeField] private ThirdPersonControllerSettingsSO config;

        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        public PlayerSettings PlayerConfig { get; private set; }

        public GroundedSettings GroundedConfig { get; private set; }

        public CameraSettings CameraConfig { get; private set; }

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

        private Animator _animator;
        private CharacterController _controller;
        private ThirdPersonInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;
        private SphereGroundChecker groundChecker;
        private DebugSphereGroundChecker groundCheckerDebug;
        public bool IsGrounded;

        public bool RotateOnMove { get; set; } = false;

        private void Awake()
        {
            if(_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

            if(config != null)
            {
                PlayerConfig = config.PlayerConfig;

                GroundedConfig = config.GroundedConfig;
                groundChecker = new SphereGroundChecker(transform.Decorate()) {
                    GroundLayers = GroundedConfig.GroundLayers,
                    GroundOffset = GroundedConfig.GroundedOffset,
                    GroundRadius = GroundedConfig.GroundedRadius
                };
                groundCheckerDebug = new DebugSphereGroundChecker(groundChecker);

                CameraConfig = config.CameraConfig;
            }
        }

        private void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<ThirdPersonInputs>();

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = PlayerConfig.JumpTimeout;
            _fallTimeoutDelta = PlayerConfig.FallTimeout;
        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);

            JumpAndGravity();

            IsGrounded = groundChecker.CheckGround();
            groundCheckerDebug.Draw();

            if(_hasAnimator)
                _animator.SetBool(_animIDGrounded, groundChecker.IsGrounded);

            Move();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void CameraRotation()
        {
            var cameraMoved = _input.look.sqrMagnitude >= _threshold
                && !CameraConfig.LockCameraPosition;
            if(cameraMoved)
            {
                _cinemachineTargetYaw += _input.look.x * CameraConfig.Sensitivity * Time.deltaTime;
                _cinemachineTargetPitch += _input.look.y * CameraConfig.Sensitivity * Time.deltaTime;
            }

            _cinemachineTargetYaw = ClampAngle(
                _cinemachineTargetYaw, float.MinValue, float.MaxValue
            );
            _cinemachineTargetPitch = ClampAngle(
                _cinemachineTargetPitch, CameraConfig.BottomClamp, CameraConfig.TopClamp
            );

            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(
                _cinemachineTargetPitch + CameraConfig.CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f
            );
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            var targetSpeed = _input.sprint ? PlayerConfig.SprintSpeed : PlayerConfig.MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if(_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            var currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            var speedOffset = 0.1f;
            var inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if(currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * PlayerConfig.SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * PlayerConfig.SpeedChangeRate);

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if(_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z)
                    * Mathf.Rad2Deg
                    + _mainCamera.transform.eulerAngles.y;

                var rotation = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y,
                    _targetRotation,
                    ref _rotationVelocity,
                    PlayerConfig.RotationSmoothTime
                );

                if(RotateOnMove)
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            if(_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void JumpAndGravity()
        {

            if(groundChecker.IsGrounded)
                CharacterOnGroundHandler();
            else
                CharacterInTheAirHandler();

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if(_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += PlayerConfig.Gravity * Time.deltaTime;
            }
        }

        private void CharacterInTheAirHandler()
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = PlayerConfig.JumpTimeout;

            // fall timeout
            if(_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // update animator if using character
                if(_hasAnimator)
                {
                    _animator.SetBool(_animIDFreeFall, true);
                }
            }

            // if we are not grounded, do not jump
            _input.jump = false;
        }

        private void CharacterOnGroundHandler()
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = PlayerConfig.FallTimeout;

            // update animator if using character
            if(_hasAnimator)
            {
                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDFreeFall, false);
            }

            // stop our velocity dropping infinitely when grounded
            if(_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if(_input.jump && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(PlayerConfig.JumpHeight * -2f * PlayerConfig.Gravity);

                // update animator if using character
                if(_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, true);
                }
            }

            // jump timeout
            if(_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if(lfAngle < -360f) lfAngle += 360f;
            if(lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}