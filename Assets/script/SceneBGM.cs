using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SceneBGM : MonoBehaviour
{
    public static SceneBGM instance;   // singleton

    [Header(" Masukkan musik untuk scene ini")]
    public AudioClip bgmClip;

    private AudioSource audioSource;

    private void Awake()
    {
        // Cegah duplikasi BGM
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.loop = true;
            audioSource.playOnAwake = false;

            // MAINKAN BGM DI AWAL
            PlayBGM();
        }
        else
        {
            Debug.LogWarning("Belum ada AudioClip di SceneBGM pada " + gameObject.name);
        }
    }

    public void StopBGM()
    {
        if (audioSource != null)
            audioSource.Stop();
    }

    public void PlayBGM()
    {
        if (audioSource != null && audioSource.clip != null)
            audioSource.Play();
    }
}
