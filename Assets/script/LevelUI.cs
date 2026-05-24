using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        levelText.text = sceneName.Replace("Level", "Level ");
    }
}