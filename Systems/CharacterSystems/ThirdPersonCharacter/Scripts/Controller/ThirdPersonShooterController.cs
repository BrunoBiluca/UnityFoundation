using Cinemachine;
using UnityEngine;
using UnityFoundation.Code.Features;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.ThirdPersonCharacter
{
    public class ThirdPersonShooterController : MonoBehaviour
    {
        [field: SerializeField] public ThridPersonShooterSettings Config { get; private set; }

        [SerializeField] CinemachineVirtualCamera aimCamera;
        [SerializeField] Transform debugAimObject;
        [SerializeField] Transform projectileSpawner;

        private ThirdPersonController thirdPersonController;
        private ThirdPersonInputs input;
        private Animator animator;

        private ICamera mainCamera;
        private IRaycastHandler raycaster;
        private IObjectPooling projectilePool;

        private void Start()
        {
            thirdPersonController = GetComponent<ThirdPersonController>();
            input = GetComponent<ThirdPersonInputs>();
            animator = GetComponent<Animator>();

            mainCamera = Camera.main.Decorate();
            raycaster = new RaycastHandler(mainCamera);

            Setup(GetComponent<IObjectPooling>());
        }

        public void Setup(IObjectPooling projectilePool)
        {
            this.projectilePool = projectilePool;
            projectilePool.InstantiateObjects();
        }

        private void Update()
        {
            var screenToworldPosition = raycaster
                .GetWorldPosition(mainCamera.ScreenCenter(), Config.aimLayerMask);

            if(!screenToworldPosition.IsPresentAndGet(out Vector3 worldPosition))
                return;

            if(debugAimObject != null)
                debugAimObject.position = worldPosition;

            aimCamera.gameObject.SetActive(input.aim);

            if(input.aim)
            {
                thirdPersonController.CameraConfig.Sensitivity = Config.aimSensitivity;
                thirdPersonController.RotateOnMove = false;

                var worldAimTarget = worldPosition;
                worldAimTarget.y = transform.position.y;
                var aimDirection = (worldAimTarget - transform.position).normalized;

                transform.forward = Vector3.Lerp(
                    transform.forward,
                    aimDirection,
                    Time.deltaTime * Config.rotateCharacterSpeed
                );

                animator.SetLayerWeight(
                    1,
                    Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * Config.aimSpeed)
                );
            }
            else
            {
                thirdPersonController.CameraConfig.Sensitivity = Config.normalSensitivity;
                thirdPersonController.RotateOnMove = true;
                animator.SetLayerWeight(
                    1,
                    Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * Config.aimSpeed)
                );
            }

            TryInstantiateProjectile(worldPosition);
        }

        private void TryInstantiateProjectile(Vector3 worldPosition)
        {
            if(!input.shoot)
                return;

            projectilePool.GetAvailableObject((o) => {
                o.transform.position = projectileSpawner.position;
                o.transform.LookAt(worldPosition);
            });

            input.shoot = false;
        }
    }
}