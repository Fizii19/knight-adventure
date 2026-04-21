using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioClip menuMusic;
    public AudioClip levelMusic;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton agar tidak double
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Play musik pertama kali
        PlayMusic(SceneManager.GetActiveScene().name);

        // Dengarkan event perpindahan scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (audioSource == null) return;

        // MAIN MENU → SELALU PUTAR ULANG
        if (scene.name == "MainMenu")
        {
            PlayMusic("MainMenu", true);
            return;
        }

    // LEVEL
        if (scene.name.StartsWith("Level"))
        {
            PlayMusic(scene.name, true);
        }
    }

    public void StopMusic()
{
    if (audioSource != null)
        audioSource.Stop();
}


    void PlayMusic(string sceneName, bool forceRestart = false)
    {
        AudioClip clipToPlay = null;

        // Musik menu
        if (sceneName == "MainMenu")
            clipToPlay = menuMusic;

        // Musik level
        else if (sceneName.StartsWith("Level"))
            clipToPlay = levelMusic;

        // Kalau ada musik yang dipilih
        if (clipToPlay != null)
        {
            // FORCE restart (dipakai saat restart level)
            if (forceRestart)
            {
                audioSource.clip = clipToPlay;
                audioSource.Play();
                return;
            }

            // Kalau bukan force restart, cek dulu apakah clip beda
            if (audioSource.clip != clipToPlay)
            {
                audioSource.clip = clipToPlay;
                audioSource.Play();
            }

        }
    }
}
