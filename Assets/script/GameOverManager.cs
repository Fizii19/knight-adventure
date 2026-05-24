using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    
    [Header("Player Components")]
    public PlayerMovement movement;
    public PlayerHealth health;

    public void ShowGameOver()
    {
        // 1. Validasi keamanan (agar tidak error jika canvas lupa di-drag di Inspector)
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
        else
        {
            Debug.LogError("GameOverCanvas belum dimasukkan ke dalam Inspector script GameOverManager!");
            return;
        }

        // 2. Matikan script pergerakan player agar tidak bisa bergerak saat mati
        if (movement != null)
        {
            movement.enabled = false; 
        }

        // 3. Pause jalannya game fisika/waktu
        Time.timeScale = 0f; 
    }

    public void RestartGame()
    {
        // PENTING: Kembalikan waktu ke 1f SEBELUM load scene agar scene baru tidak ikut beku
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (SceneBGM.instance != null)
        {
            SceneBGM.instance.PlayBGM();
        }
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}