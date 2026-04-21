using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishTrigger : MonoBehaviour
{
    public AudioSource finishSound;
    public AudioClip finishSFX;
    public GameObject finishCanvas;
    private Animator animChest;
    private bool isFinished = false;
    public float delayTime = 6f;

    private void Start()
    {
        animChest = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFinished && collision.CompareTag("Player"))
        {
            // Cek syarat 1: Kunci
            PlayerKey playerKey = collision.GetComponent<PlayerKey>();
            if (playerKey == null || !playerKey.HasKey())
            {
                Debug.Log("Syarat belum terpenuhi: Kamu butuh kunci!");
                return;
            }

            // Cek syarat 2: Bunuh Enemy (Hanya untuk Level 3 ke atas)
            if (GameManager.instance != null && GameManager.instance.currentLevel >= 3)
            {
                if (GameManager.instance.enemiesKilled < GameManager.instance.enemiesRequired)
                {
                    Debug.Log("Syarat belum terpenuhi: Kamu harus mengalahkan enemy!");
                    return;
                }
            }

            isFinished = true;

            if (BGMManager.instance != null)
                BGMManager.instance.StopMusic();


            // Fade out BGM
            StartCoroutine(FadeOutBGM(0.5f));

            // Play finish sound
            if (finishSFX != null && finishSound != null)
                finishSound.PlayOneShot(finishSFX);

            // Disable movement
            PlayerMovement pm = collision.GetComponent<PlayerMovement>();
            if (pm != null) pm.enabled = false;

            // Freeze player physics
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            // Force idle animation
            Animator anim = collision.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetFloat("Speed", 0f);
                anim.SetBool("isJumping", false);
            }

                animChest.SetTrigger("Finish");

            StartCoroutine(DelayFinish());
        }
    }

    private IEnumerator DelayFinish()
    {
        yield return new WaitForSeconds(delayTime);

        if (finishCanvas != null)
            finishCanvas.SetActive(true);
    }

    private IEnumerator FadeOutBGM(float duration)
    {
        if (SceneBGM.instance == null) yield break;
        AudioSource bgm = SceneBGM.instance.GetComponent<AudioSource>();
        if (bgm == null) yield break;

        float startVolume = bgm.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgm.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        bgm.volume = 0f;
        bgm.Stop();
    }
}
