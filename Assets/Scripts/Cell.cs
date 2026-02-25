using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2 coord = new Vector2();
    public bool blockOverlay = false;
    public bool isOccupied = false;
    public bool playParticleEffect = false;
    public ParticleSystem ParticleSystem;

    private SpriteRenderer spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ParticleSystem = GetComponent<ParticleSystem>();
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
            playParticleEffect = true;
        }
        else
        {
            spriteRenderer.color = Color.gray;

            if(playParticleEffect)
            {
                Debug.Log("Kaboom");
                ParticleSystem.Play();
                playParticleEffect = false;
            }
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
