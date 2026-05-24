using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioClip bgm;

    void Start()
    {
        SoundManager.Instance.PlayBGM(bgm);
    }
}