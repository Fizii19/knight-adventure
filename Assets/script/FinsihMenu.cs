using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    public string nextLevelName; // isi dari Inspector
    public string mainMenuName;  // isi dari Inspector

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("woi");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
