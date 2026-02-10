using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject childPrefab;
    public int[,] array = new int[8, 8];
    private Cell cell;

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
                newChild.GetComponent<Cell>().coord[0] = i;
                newChild.GetComponent<Cell>().coord[1] = j;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnCellOn(int[] cell)
    {
        array[cell[0], cell[1]] = 1;
        Debug.Log(cell[0] + " , " + cell[1]);
    }

    public void TurnCellOff(int[] cell)
    {
        array[cell[0], cell[1]] = 0;
    }
}
