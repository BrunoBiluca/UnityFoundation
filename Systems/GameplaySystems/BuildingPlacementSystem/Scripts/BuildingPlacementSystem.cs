using UnityFoundation.Code;
using Assets.UnityFoundation.Systems.ObjectPooling;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code.Grid.ObjectPlacementGrid;
using UnityFoundation.Code.Grid;
using UnityFoundation.Code.DebugHelper;

namespace Assets.UnityFoundation.Systems.BuildingPlacementSystem
{
    public class BuildingPlacementSystem : Singleton<BuildingPlacementSystem>
    {
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 10;

        // TODO: atualmente funciona bem com cellSize de tamanho 4
        //  todos os prefabs tem que ter escala XZ em 4
        //  o floor deve ter escala XZ 0.4 e posi��o XZ 2
        [SerializeField] private int cellSize = 4;

        [SerializeField] private MultipleObjectPooling buildingPooling;
        [SerializeField] private List<GridObjectSO> buildings;
        [SerializeField] private Transform floor;

        public event Action<GridObjectSO> OnCurrentSelectedBuildingChange;

        private GridObjectDirection currentDirection;
        private GridObjectSO currentBuilding;
        private ObjectPlacementGrid grid;

        private GridObjectSO CurrentBuilding {
            get { return currentBuilding; }
            set {
                currentBuilding = value;
                OnCurrentSelectedBuildingChange?.Invoke(currentBuilding);
            }
        }

        protected override void OnAwake()
        {
            grid = new ObjectPlacementGrid(width, height, cellSize);

            CurrentBuilding = buildings[0];
            currentDirection = GridObjectDirection.DOWN;
        }

        private void Start()
        {
            buildingPooling.InstantiateObjects();

            floor.transform.localScale = new Vector3(width, 1f, height);
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                CreateBuilding();
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                currentDirection = currentDirection.Next();
            }

            HotkeysInput();
        }

        private void HotkeysInput()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
                CurrentBuilding = buildings[0];
            else if(Input.GetKeyDown(KeyCode.Alpha2))
                CurrentBuilding = buildings[1];
            else if(Input.GetKeyDown(KeyCode.Alpha3))
                CurrentBuilding = buildings[2];
        }

        private void CreateBuilding()
        {
            var mousePosition = CameraUtils.GetMousePosition3D();
            var pos = grid.GetCellPosition((int)mousePosition.x, (int)mousePosition.z);
            var position = new Int2(pos.X, pos.Z);
            if(!grid.IsInsideGrid(position.X, position.Y))
            {
                DebugPopup.Create("Can't create here.");
                return;
            }

            var gridObject = new GridObject(
                CurrentBuilding.Width, CurrentBuilding.Height, currentDirection
            );

            if(!grid.TrySetGridValue(grid.GetWorldPosition(position.X, position.Y, gridObject), gridObject))
                return;

            var building = buildingPooling.GetAvailableObject(CurrentBuilding.Tag).Get();
            building
                .GetComponent<Building>()
                .Setup(gridObject)
                .Activate((go) => {
                    var gridPos = grid.GetCellPosition(position.X, position.Y);
                    go
                    .transform
                    .position = new Vector3(gridPos.X, 0f, gridPos.Z);

                    go
                    .transform
                    .rotation = Quaternion.Euler(0f, currentDirection.Rotation, 0f);
                });
        }

        public void SetCurrentBuilding(GridObjectSO building)
        {
            CurrentBuilding = building;
        }

        public void RemoveBuilding(Building building)
        {
            grid.ClearValue(building.GridObjectRef);
            building.Deactivate();
        }

        public bool CanBuild(Vector3 position, out Vector3 gridPosition, out Quaternion rotation)
        {
            var pos = grid.GetCellPosition((int)position.x, (int)position.z);

            var newGridObject = new GridObject(
                CurrentBuilding.Width, CurrentBuilding.Height, currentDirection
            );
            if(!grid.CanSetGridValue(pos, newGridObject))
            {
                gridPosition = default;
                rotation = default;
                return false;
            }

            gridPosition = grid.GetWorldPosition(pos.X, pos.Z, newGridObject);
            rotation = Quaternion.Euler(0f, currentDirection.Rotation, 0f);
            return true;
        }
    }
}