using Assets.UnityFoundation.CameraScripts;
using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.Code.DebugHelper;
using Assets.UnityFoundation.Code.Grid;
using Assets.UnityFoundation.Code.Grid.ObjectPlacementGrid;
using Assets.UnityFoundation.Code.ObjectPooling;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.BuildingPlacementSystem
{
    public class BuildingPlacementSystem : Singleton<BuildingPlacementSystem>
    {
        [SerializeField] private bool debugMode = false;
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 10;

        // TODO: atualmente funciona bem com cellSize de tamanho 4
        //  todos os prefabs tem que ter escala XZ em 4
        //  o floor deve ter escala XZ 0.4 e posição XZ 2
        [SerializeField] private int cellSize = 4;

        [SerializeField] private MultipleObjectPooling buildingPooling;
        [SerializeField] private List<GridObjectSO> buildings;
        [SerializeField] private Transform floor;

        public event Action<GridObjectSO> OnCurrentSelectedBuildingChange;

        private GridObjectDirection currentDirection;
        private GridObjectSO currentBuilding;
        private GridXZ<GridObject> grid;

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

            if(debugMode)
            {
                grid = new GridXZDebug<GridObject>(grid);
            }

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
            var position = grid.GetGridPostion(CameraUtils.GetMousePosition3D());
            if(!grid.IsInsideGrid(position.x, position.y))
            {
                DebugPopup.Create("Can't create here.");
                return;
            }

            var gridObject = new GridObject(
                CurrentBuilding.Width, CurrentBuilding.Height, currentDirection
            );

            if(!grid.TrySetGridValue(grid.GetWorldPosition(position.x, position.y), gridObject))
                return;

            var building = buildingPooling.GetAvailableObject(CurrentBuilding.Tag).Get();
            building
                .GetComponent<Building>()
                .Setup(gridObject)
                .Activate((go) => {
                    go
                    .transform
                    .position = grid.GetWorldPosition(position.x, position.y, gridObject);

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
            grid.ClearGridValue(building.GridObjectRef);
            building.Deactivate();
        }

        public bool CanBuild(Vector3 position, out Vector3 gridPosition, out Quaternion rotation)
        {
            var gridPos = grid.GetGridPostion(position);

            var newGridObject = new GridObject(
                CurrentBuilding.Width, CurrentBuilding.Height, currentDirection
            );
            if(!grid.CanSetGridValue(gridPos, newGridObject))
            {
                gridPosition = default;
                rotation = default;
                return false;
            }

            gridPosition = grid.GetWorldPosition(gridPos.x, gridPos.y, newGridObject);
            rotation = Quaternion.Euler(0f, currentDirection.Rotation, 0f);
            return true;
        }
    }
}