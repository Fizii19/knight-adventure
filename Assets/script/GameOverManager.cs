using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public GameObject gameOverCanvas;
    public PlayerMovement movement;
    public PlayerHealth health;

    public void ShowGameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f; // pause game

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneBGM.instance?.PlayBGM();

    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
