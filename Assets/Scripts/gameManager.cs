using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    public GameObject ballBlock;

    void Start(){
        ballBlock.SetActive(true);
    }
    public void StartGame()
    {
        // Unpause the game when the Start button is pressed.
        ballBlock.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        // Reload the current scene to reset the level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Ensure timeScale is set to 0 again after reloading.
        Time.timeScale = 0f;
    }
}


