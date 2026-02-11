using UnityEngine;

public class CellGrid : MonoBehaviour
{
    [SerializeField] GameObject childPrefab;

    public GameObject currentAnchorPoint = null;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D myCollider = collision.GetContact(0).otherCollider;
        GameObject collidedCell = myCollider.gameObject;
        if(collision.collider.gameObject.tag == "Anchor")
        {
            PlaceAnchorPoint(collidedCell);
        }
    }
    public void PlaceAnchorPoint(GameObject cell)
    {
        if (currentAnchorPoint == null) {
            currentAnchorPoint = cell;
            cell.GetComponent<Cell>().isAnchor = true;
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
