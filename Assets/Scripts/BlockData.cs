using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Hierarchy;
using UnityEngine;

public class BlockData
{
    public Vector2 anchor;
    //0's represent nothing, 1's represent cells
    public int[,] block_data = new int[5, 5];

    public BlockData(MyArrayWrapper[] block_data, Vector2 anchor)
    {
        this.anchor = anchor;
        this.block_data = MyArrayWrapper.convertTo2DArray(block_data);
    }

    //Returns the points around the anchor with blocks, x and y relative to the anchor
    public List<Vector2> GetData()
    {
        List<Vector2> returnArray = new List<Vector2>();
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if(this.block_data[x,y] == 1)
                {
                    returnArray.Add(new Vector2(x - anchor.x, y - anchor.y));
                }
            }
        }
        return returnArray;
    }

    public Vector2 getWidthAndHeight()
    {
        int minX = 5, maxX = -1;
        int minY = 5, maxY = -1;
        bool foundAny = false;

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (this.block_data[x, y] == 1)
                {
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                    foundAny = true;
                }
            }
        }

        if (!foundAny) return Vector2.zero;

        //Size is the distance between max and min, plus 1 for the cell itself
        float width = maxX - minX + 1;
        float height = maxY - minY + 1;

        return new Vector2(width, height);
    }
}
