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
}
