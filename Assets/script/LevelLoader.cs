using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Masukkan nama scene dari Inspector
    public string sceneName;

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(sceneName);
    }
}
