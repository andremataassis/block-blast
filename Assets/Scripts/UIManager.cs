using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverOverlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CellGrid.Instance.gameOver.AddListener(onGameOver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onGameOver()
    {
        gameOverOverlay.SetActive(true);
    }
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
