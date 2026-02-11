using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public BlockData block_data;
    [Header("Block data")]
    public MyArrayWrapper[] array;
    public Vector2 anchor;

    [Header("Block construction")]
    public GameObject cellPrefab;
    public GameObject anchorCellPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        block_data = new BlockData(array, anchor);
        createBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void createBlock()
    {
        //First, place anchor
        GameObject anchorCell = Instantiate(anchorCellPrefab, this.transform);
        anchorCell.transform.localPosition = new Vector2(0, 0);
        anchorCell.name = "Anchor";

        //Now place the things around it
        int[,] data = block_data.block_data;
        int width = data.GetLength(0);
        int height = data.GetLength(1);
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //Zeros mean nothing
                if(data[x, y] == 0) continue;
                //Skip anchor
                if(x == block_data.anchor.x &&  y == block_data.anchor.y) continue;

                GameObject newCell = Instantiate(cellPrefab, this.transform);
                Vector2 position = new Vector2(x - 2, y - 2);
                newCell.transform.localPosition = position;
            }
        }
    }

    private Vector2 offset;
    void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }
    private void OnMouseDrag()
    {
        Vector2 curPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        transform.position = curPosition;
    }
}
