using UnityFoundation.Code;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.BuildingPlacementSystem
{
    public class GhostBuilding : MonoBehaviour
    {
        private Transform ghostTransform;

        private void Start()
        {
            BuildingPlacementSystem.Instance
                .OnCurrentSelectedBuildingChange += CurrentSelectedBuildingChangeHandle;
        }

        private void CurrentSelectedBuildingChangeHandle(GridObjectSO obj)
        {
            if(ghostTransform != null)
                Destroy(ghostTransform.gameObject);

            var newGhostBuilding = Instantiate(obj.Prefab, transform);

            // Necessário criar um Renderer para alterar as propriedades de visualização
            TransformUtils.ChangeLayer(
                newGhostBuilding.transform, LayerMask.NameToLayer("Ghost")
            );

            ghostTransform = newGhostBuilding.transform;
        }

        private void Update()
        {
            if(ghostTransform == null) return;

            var canBuild = BuildingPlacementSystem
                .Instance
                .CanBuild(
                    CameraUtils.GetMousePosition3D(),
                    out Vector3 buildingPosition,
                    out Quaternion buildingRotation
                );

            if(canBuild)
            {
                ghostTransform.gameObject.SetActive(true);
                transform.position = buildingPosition;
                transform.rotation = buildingRotation;
            }
            else
            {
                ghostTransform.gameObject.SetActive(false);
            }


        }
    }
}