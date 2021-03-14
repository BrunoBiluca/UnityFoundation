using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralPlatformBuilder : MonoBehaviour {
    private const int gridWidth = 200;
    private const int gridHeight = 20;

    public Tilemap tilemap;

    public TileBase tileTop;
    public TileBase tileBase;

    private int[,] map;

    public void Start() {
        tilemap = transform.Find("platformTilemap").GetComponent<Tilemap>();
        map = new RandomWalkTopSmoothed(gridWidth, gridHeight).GenerateGrid(4, 8);

        transform.position = new Vector3(-gridWidth / 2, -gridHeight / 2, 0);

        RenderMap();
    }

    public void RenderMap() {
        tilemap.ClearAllTiles();
        for(int x = 0; x < map.GetUpperBound(0); x++) {
            for(int y = 0; y < map.GetUpperBound(1); y++) {
                if(map[x, y] == TileStatus.filled) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), FillTile(x, y));
                }
            }
        }
    }

    private TileBase FillTile(int x, int y) {
        var aboveTile = y + 1;
        if(aboveTile == map.GetUpperBound(1)) {
            return tileTop;
        }

        if(aboveTile < map.GetUpperBound(1) && map[x, aboveTile] == TileStatus.empty) {
            return tileTop;
        }
        return tileBase;
    }

}
