using System.Collections.Generic;
using System.Linq;
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
                if(block_data[x,y] == 1)
                {
                    returnArray.Append(new Vector2(x - anchor.x, y - anchor.y));
                }
            }
        }
        return returnArray;
    }
}
