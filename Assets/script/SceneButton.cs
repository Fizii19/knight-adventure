using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    [Header("Nama scene yang ingin dituju")]
    public string sceneName; // ← pastikan 'public' di sini

    public void GantiScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Nama scene belum diisi di Inspector!");
        }
    }
}
