using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2 coord = new Vector2();
    public bool isAnchor = false;

    private SpriteRenderer spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnchor)
        {
            spriteRenderer.color = Color.red;
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
            CellGrid.Instance.RemoveAnchorPoint(this.gameObject);
        }
    }
}
