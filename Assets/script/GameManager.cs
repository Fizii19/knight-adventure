using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Diamond System")]
    public int diamondCount = 0;
    public int requiredDiamonds = 3;

    [Header("Finish")]
    public GameObject finishLine;
    public int enemiesKilled = 0;
    public int enemiesRequired = 1;
    public int currentLevel;

    [Header("UI")]
    public GameObject finishPanel;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        finishLine?.SetActive(false);
        finishPanel?.SetActive(false);

        // Otomatis deteksi level dari nama scene (Format: "Level1", "Level2", dst)
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (sceneName.StartsWith("Level"))
        {
            int.TryParse(sceneName.Replace("Level", ""), out currentLevel);
        }
    }

    public void AddDiamond()
    {
        diamondCount++;

        if (diamondCount >= requiredDiamonds)
        {
            finishLine.SetActive(true);
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }

    public void LevelComplete()
    {
        finishPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
