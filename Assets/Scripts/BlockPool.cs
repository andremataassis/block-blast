using UnityEngine;

public class BlockPool : MonoBehaviour
{
    [Header("Block Pool")]
    [SerializeField] GameObject[] blockPrefabs;

    [Header("Attributes")]
    public GameObject[] blocksOut = new GameObject[3];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        populatePool();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkIfEmpty())
        {
            populatePool();
        }
    }

    bool checkIfEmpty()
    {
        bool empty = true;
        foreach (var block in blocksOut)
        {
            if (block == null) { }
            else empty = false;
        }
        return empty;
    }

    //Populates the pool with blocks
    void populatePool()
    {
        for (int i = 0; i < 3; i++) {
            int newBlockID = Random.Range(0, blockPrefabs.Length);
            GameObject newBlock = Instantiate(blockPrefabs[newBlockID], this.transform);

            newBlock.transform.position = this.transform.position;
            newBlock.transform.localPosition = new Vector2 (-3 + i*3, 0);

            blocksOut[i] = newBlock;
        }
    }
}
