using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons; // Isi 5 tombol Level1–5

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Untuk setiap tombol
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 <= unlockedLevel)
            {
                levelButtons[i].interactable = true;   // Level bisa dibuka
            }
            else
            {
                levelButtons[i].interactable = false;  // Level terkunci
            }
        }
    }

    // Fungsi tombol level
    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene("Level" + levelNumber);
    }
}
