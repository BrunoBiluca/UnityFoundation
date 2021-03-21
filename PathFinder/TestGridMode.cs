using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class TestGridMode : MonoBehaviour {

    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;

    GridMode grid;

    List<Vector3> mouseInputs;

    PathFinding pathFindingGrid;

    void Start() {

        grid = new GridMode(gridWidth, gridHeight);
        pathFindingGrid = new PathFinding(new int2(gridWidth, gridHeight), debug: false);

        mouseInputs = new List<Vector3>();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            SelectGridPosition();
        }

        if(Input.GetMouseButtonDown(1)) {
            ToogleBlockGridPosition();
        }
    }

    private void ToogleBlockGridPosition() {
        var mouse = Input.mousePosition;
        var worldPosition = Camera.main.ScreenToWorldPoint(mouse);
        grid.SetNodeValue(worldPosition, "###");
        pathFindingGrid.SetIsWalkable(grid.GetGridPostion(worldPosition), false);
    }

    private void SelectGridPosition() {
        var mouse = Input.mousePosition;
        var worldPosition = Camera.main.ScreenToWorldPoint(mouse);
        grid.SetNodeValue(worldPosition, "32");

        if(!grid.IsWolrdPositionInsideGrid(worldPosition))
            return;

        mouseInputs.Add(worldPosition);
        if(mouseInputs.Count == 2) {
            var startPos = grid.GetGridPostion(mouseInputs.First());
            var endPos = grid.GetGridPostion(mouseInputs.Last());
            var path = pathFindingGrid.FindPath(new int2(startPos), new int2(endPos));

            mouseInputs.Clear();
            if(path.Count() == 1) {
                Debug.Log("Caminho não encontrado");
                return;
            }

            foreach(var p in path) {
                Debug.Log(p);
            }

            grid.DrawLines(path.ToArray());
        }
    }
}
