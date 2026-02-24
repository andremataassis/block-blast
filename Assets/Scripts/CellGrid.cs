using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    [SerializeField] GameObject childPrefab;

    //The current anchor point ON THE GRID
    public GameObject currentAnchorPoint = null;

    #region SINGLETON
    public static CellGrid Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }
    #endregion SINGLETON

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                GameObject newChild = Instantiate(childPrefab, this.transform);
                Vector2 position = new Vector2();
                position.x = -3.5f + i;
                position.y = -3.5f + j;
                newChild.transform.position = position;
                newChild.name = "(" + (i + 1) + ", " + (j + 1) + ")";
                newChild.GetComponent<Cell>().coord = new Vector2(i, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D myCollider = collision.GetContact(0).otherCollider;
        GameObject myCollidedCell = myCollider.gameObject;

        if(collision.collider.gameObject.tag == "Anchor")
        {
            PlaceAnchorPoint(myCollidedCell, collision.collider.gameObject);
        }
    }
    public void PlaceAnchorPoint(GameObject anchorCell_grid, GameObject anchorCell_block)
    {
        if (currentAnchorPoint == null) {
            //Get cells around anchor point
            BlockData data = anchorCell_block.transform.parent.GetComponent<Block>().block_data;
            List<Vector2> list = data.GetData();

            Vector2 anchorCoord_grid = anchorCell_grid.GetComponent<Cell>().coord;

            //Try to place them
            foreach(Vector2 cellAround in list)
            {
                Transform cellOnGrid = transform.Find($"({anchorCoord_grid.x + cellAround.x}, {anchorCoord_grid.y + cellAround.y})");
                if (cellOnGrid == null)
                {
                    return;
                }
                else
                {
                    Debug.Log(cellOnGrid);
                    cellOnGrid.GetComponent<Cell>().hasBlock = true;
                }
            }

            currentAnchorPoint = anchorCell_grid;
            anchorCell_grid.GetComponent<Cell>().isAnchor = true;
        }
    }

    public void RemoveAnchorPoint(GameObject cell)
    {
        if (cell == currentAnchorPoint)
        {
            cell.GetComponent<Cell>().isAnchor = false;
            currentAnchorPoint = null;
        }
    }
}
