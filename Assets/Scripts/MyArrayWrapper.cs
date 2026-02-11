using UnityEngine;

[System.Serializable]
public class MyArrayWrapper
{
    public int[] columns = new int[5];

    public static int[,] convertTo2DArray(MyArrayWrapper[] rows)
    {
        if (rows == null || rows.Length == 0) return new int[0, 0];

        int rowCount = rows.Length;
        int colCount = rows[0].columns.Length;

        int[,] result = new int[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                // Safety check in case rows have different lengths (jagged)
                if (j < rows[i].columns.Length)
                {
                    result[i, j] = rows[i].columns[j];
                }
            }
        }

        return result;
    }
}
