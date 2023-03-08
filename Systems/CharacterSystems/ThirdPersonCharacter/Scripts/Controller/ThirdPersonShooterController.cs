using Cinemachine;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.ThirdPersonCharacter
{
    public class ThirdPersonShooterController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera aimCamera;
        [SerializeField] float normalSensitivity = 0.8f;
        [SerializeField] float aimSensitivity = 0.1f;
        [SerializeField] float aimSpeed = 10f;
        [SerializeField] private float rotateCharacterSpeed = 10f;

        [SerializeField] Transform debugAimObject;
        [SerializeField] Transform projectileSpawner;
        [SerializeField] GameObject projectilePrefab;

        private ThirdPersonController thirdPersonController;
        private ThirdPersonInputs input;
        private Animator animator;

        private void Start()
        {
            thirdPersonController = GetComponent<ThirdPersonController>();
            input = GetComponent<ThirdPersonInputs>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var worldPosition = CameraUtils.GetWorldPosition3D(
                CameraUtils.ScreenCenter()
            );

            if(debugAimObject != null)
                debugAimObject.position = worldPosition;

            aimCamera.gameObject.SetActive(input.aim);

            if(input.aim)
            {
                thirdPersonController.CameraConfig.Sensitivity = aimSensitivity;
                thirdPersonController.RotateOnMove = false;

                var worldAimTarget = worldPosition;
                worldAimTarget.y = transform.position.y;
                var aimDirection = (worldAimTarget - transform.position).normalized;

                transform.forward = Vector3.Lerp(
                    transform.forward,
                    aimDirection,
                    Time.deltaTime * rotateCharacterSpeed
                );

                animator.SetLayerWeight(
                    1,
                    Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * aimSpeed)
                );
            }
            else
            {
                thirdPersonController.CameraConfig.Sensitivity = normalSensitivity;
                thirdPersonController.RotateOnMove = true;
                animator.SetLayerWeight(
                    1,
                    Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * aimSpeed)
                );
            }

            if(input.shoot)
            {
                var projectileDirection
                    = (worldPosition - projectileSpawner.position).normalized;

                Instantiate(
                    projectilePrefab,
                    projectileSpawner.position,
                    Quaternion.LookRotation(projectileDirection, Vector3.up)
                );
                input.shoot = false;
            }
        }
    }
}