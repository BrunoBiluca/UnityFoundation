using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkTopSmoothed {

    private int width;
    private int height;
    private int[,] map;

    public RandomWalkTopSmoothed(int width, int height) {
        this.width = width;
        this.height = height;

        map = GenerateArray(width, height, true);
    }

    public int[,] GenerateArray(int width, int height, bool empty) {
        int[,] map = new int[width, height];
        for(int x = 0; x < map.GetUpperBound(0); x++) {
            for(int y = 0; y < map.GetUpperBound(1); y++) {
                if(empty) {
                    map[x, y] = TileStatus.empty;
                } else {
                    map[x, y] = TileStatus.filled;
                }
            }
        }
        return map;
    }

    public int[,] GenerateGrid(
        int minSectionWidth,
        int maxSectionWidth = 0
    ) {
        if(maxSectionWidth == 0) maxSectionWidth = minSectionWidth;

        System.Random rand = new System.Random(DateTime.Now.Ticks.GetHashCode());

        int lastHeight = UnityEngine.Random.Range(0, map.GetUpperBound(1));

        int sectionWidth = 0;

        var currSectionWitdth = rand.Next(minSectionWidth, maxSectionWidth);
        for(int x = 0; x <= map.GetUpperBound(0); x++) {
            int nextMove = rand.Next(2);

            if(nextMove == 0 && lastHeight > 0 && sectionWidth > currSectionWitdth) {
                lastHeight--;
                sectionWidth = 0;
                currSectionWitdth = rand.Next(minSectionWidth, maxSectionWidth);
            } else if(nextMove == 1
                && lastHeight < map.GetUpperBound(1) && sectionWidth > currSectionWitdth
            ) {
                lastHeight++;
                sectionWidth = 0;
                currSectionWitdth = rand.Next(minSectionWidth, maxSectionWidth);
            }
            sectionWidth++;

            for(int y = lastHeight; y >= 0; y--) {
                map[x, y] = 1;
            }
        }

        return map;
    }
}
