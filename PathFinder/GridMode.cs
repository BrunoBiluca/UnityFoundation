using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;

public class GridMode {
    private int width;
    private int height;
    private int cellSize;

    private int[,] gridArray;
    private TextMesh[,] gridTextArray;

    public GridMode(int width, int height) {
        this.width = width;
        this.height = height;

        gridArray = new int[width, height];
        gridTextArray = new TextMesh[width, height];

        cellSize = 4;

        for(int x = 0; x < gridArray.GetLength(0); x++) {
            for(int y = 0; y < gridArray.GetLength(1); y++) {
                gridTextArray[x, y] = CreateWordText(gridArray[x, y].ToString(), GetWorldPosition(x, y));
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize;
    }

    public Vector2 GetGridPostion(Vector3 position) {
        var gridPosition = new Vector2(
            (int)Math.Floor(position.x / cellSize),
            (int)Math.Floor(position.y / cellSize)
        );

        return gridPosition;
    }

    public bool IsInsideGrid(int x, int y) {
        return x >= 0 && x < width
            && y >= 0 && y < height;
    }

    public bool IsInsideGrid(Vector3 pos) {
        return IsInsideGrid((int)pos.x, (int)pos.y);
    }

    public bool IsInsideGrid(params Vector3[] positions) {
        foreach(var pos in positions) {
            if(!IsInsideGrid(pos)) {
                return false;
            }
        }
        return true;
    }

    public bool IsWolrdPositionInsideGrid(params Vector3[] positions) {
        foreach(var pos in positions) {
            if(!IsInsideGrid(GetGridPostion(pos)))
                return false;
        }
        return true;
    }

    public TextMesh CreateWordText(string text, Vector3 localPosition, Transform parent = null) {
        var gameObject = new GameObject("World_Text", typeof(TextMesh));

        var transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition + new Vector3(cellSize / 2, cellSize / 2);


        var textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = Color.white;
        textMesh.fontSize = 16;
        textMesh.anchor = TextAnchor.MiddleCenter;

        return textMesh;
    }

    public bool SetNodeValue(Vector3 position, string value) {
        var gridPosition = GetGridPostion(position);

        if(!IsInsideGrid((int)gridPosition.x, (int)gridPosition.y))
            return false;

        gridTextArray[(int)gridPosition.x, (int)gridPosition.y].text = value;

        return true;
    }

    public bool DrawLines(params Vector3[] positions) {

        var gridPositions = new List<Vector3>();
        foreach(var pos in positions) {
            gridPositions.Add(GetGridPostion(pos));
        }

        if(!IsInsideGrid(gridPositions.ToArray())) {
            return false;
        }

        for(int i = 1; i < gridPositions.Count; i++) {
            Debug.DrawLine(
                GetWorldPosition((int)gridPositions[i - 1].x, (int)gridPositions[i - 1].y) + new Vector3(cellSize / 2, cellSize / 2, 0),
                GetWorldPosition((int)gridPositions[i].x, (int)gridPositions[i].y) + new Vector3(cellSize / 2, cellSize / 2, 0),
                Color.white,
                100f
            );
        }
        return true;
    }

    public bool DrawLines(params int2[] gridPositions) {
        for(int i = 1; i < gridPositions.Length; i++) {
            var centerNodeCorrection = new Vector3(cellSize / 2, cellSize / 2, 0);

            Debug.DrawLine(
                GetWorldPosition(gridPositions[i - 1].x, gridPositions[i - 1].y) + centerNodeCorrection,
                GetWorldPosition(gridPositions[i].x, gridPositions[i].y) + centerNodeCorrection,
                Color.white,
                1000000
            );
        }
        return true;
    }

}
