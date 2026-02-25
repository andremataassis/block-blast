using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2 coord = new Vector2();
    public bool blockOverlay = false;
    public bool isOccupied = false;

    private SpriteRenderer spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (blockOverlay)
        {
            spriteRenderer.color = Color.red;
        }
        else if (isOccupied)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.gray;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Anchor")
        {
            CellGrid.Instance.RemoveBlockOverlay(this.gameObject, collision.collider.gameObject);
        }
    }
}
