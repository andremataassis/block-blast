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

    private Vector2 initialPos;
    public BoxCollider2D BoxCollider;
    void Awake()
    {
        block_data = new BlockData(array, anchor);
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        initialPos = transform.position;
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

        //Fix collider based on shape
        Vector2 scale = block_data.getWidthAndHeight();
        BoxCollider.size = scale;
        float x_offset = scale.x % 2 == 0 ? 0.5f : 0;
        float y_offset = scale.y % 2 == 0 ? 0.5f : 0;
        BoxCollider.offset = new Vector2(x_offset, y_offset);
    }

    #region Click and Drag
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
    #endregion Click and Drag

    //Placing block
    private void OnMouseUp()
    {
        bool result = CellGrid.Instance.placeBlock();
        if (result == false)
        {
            transform.position = initialPos;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
