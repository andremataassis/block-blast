using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        if (transform.childCount <= 1)
        {
            //destroy all children (gets rid of placeholder)
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = transform.GetChild(i).gameObject;
                Destroy(child);
            }

            //Make grid
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    GameObject newChild = Instantiate(childPrefab, this.transform);
                    Vector2 position = new Vector2();
                    position.x = -3.5f + i;
                    position.y = -3.5f + j;
                    newChild.transform.localPosition = position;
                    newChild.name = "(" + (i) + ", " + (j) + ")";
                    newChild.GetComponent<Cell>().coord = new Vector2(i, j);
                }
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
            PlaceBlockOverlay(myCollidedCell, collision.collider.gameObject);
        }
    }
    public void PlaceBlockOverlay(GameObject anchorCell_grid, GameObject anchorCell_block)
    {
        if (currentAnchorPoint == null) {
            //Get cells around anchor point
            List<Vector2> list = anchorCell_block.transform.parent.GetComponent<Block>().block_data.GetData();

            Vector2 anchorCoord_grid = anchorCell_grid.GetComponent<Cell>().coord;

            //Check that they can be placed
            foreach(Vector2 cellAround in list)
            {
                Transform cellOnGrid = transform.Find($"({anchorCoord_grid.x + cellAround.x}, {anchorCoord_grid.y + cellAround.y})");
                if (cellOnGrid == null)
                {
                    return;
                }
                else if (cellOnGrid.GetComponent<Cell>().isOccupied)
                {
                    return;
                }
            }

            //Place them :)
            foreach (Vector2 cellAround in list)
            {
                Transform cellOnGrid = transform.Find($"({anchorCoord_grid.x + cellAround.x}, {anchorCoord_grid.y + cellAround.y})");
                cellOnGrid.GetComponent<Cell>().blockOverlay = true;
            }

            currentAnchorPoint = anchorCell_grid;
        }
    }

    public void RemoveBlockOverlay(GameObject anchorCell_grid, GameObject anchorCell_block)
    {
        if (anchorCell_grid == currentAnchorPoint)
        {
            //Get cells around anchor point
            List<Vector2> list = anchorCell_block.transform.parent.GetComponent<Block>().block_data.GetData();

            Vector2 anchorCoord_grid = anchorCell_grid.GetComponent<Cell>().coord;

            //Remove overlay blocks
            foreach (Vector2 cellAround in list)
            {
                Transform cellOnGrid = transform.Find($"({anchorCoord_grid.x + cellAround.x}, {anchorCoord_grid.y + cellAround.y})");
                cellOnGrid.GetComponent<Cell>().blockOverlay = false;
            }

            currentAnchorPoint = null;
        }
    }

    //Returns true if block is placed
    public bool placeBlock()
    {
        if(currentAnchorPoint == null)
        {
            return false;
        }
        else
        {
            foreach(Transform child in transform)
            {
                Cell cellComponent = child.GetComponent<Cell>();
                bool overlayed = cellComponent.blockOverlay;
                if (overlayed)
                {
                    cellComponent.isOccupied = true;
                    cellComponent.playParticleEffect = true;
                    cellComponent.blockOverlay = false;
                }
            }
            currentAnchorPoint = null;
            checkForClear();
            return true;
        }
    }

    public void checkForClear()
    {
        int[] countPerX = new int[8];
        int[] countPerY = new int[8];
        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 8; y++)
            {
                Transform cellOnGrid = transform.Find($"({x}, {y})");
                if (cellOnGrid.GetComponent<Cell>().isOccupied)
                {
                    countPerX[y] += 1;
                    countPerY[x] += 1;
                }
            }
        }

        for(int i = 0; i < 8; i++) {
            if (countPerX[i] == 8)
            {
                clearRow(i);
            }
            if (countPerY[i] == 8)
            {
                clearColumn(i);
            }
        }
    }

    public void clearRow(int y)
    {
        for (int i = 0; i < 8; i++) 
        {
            Transform cellOnGrid = transform.Find($"({i}, {y})");
            cellOnGrid.GetComponent<Cell>().isOccupied = false;
        }
    }

    public void clearColumn(int x) 
    {
        for (int i = 0; i < 8; i++)
        {
            Transform cellOnGrid = transform.Find($"({x}, {i})");
            cellOnGrid.GetComponent<Cell>().isOccupied = false;
        }
    }
}
