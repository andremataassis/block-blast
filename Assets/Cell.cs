using UnityEngine;

public class Cell : MonoBehaviour
{
    public int[] coord = new int[2];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (collision.gameObject.tag == "BlockCell") {
            if (Vector2.Distance(other.transform.position, this.transform.position) < 0.2f)
            {
                this.GetComponent<SpriteRenderer>().color = Color.blue;
                SendMessageUpwards("TurnCellOn", coord);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "BlockCell")
        {
            this.GetComponent<SpriteRenderer>().color = Color.gray;
            SendMessageUpwards("TurnCellOff", coord);
        }
    }
}
