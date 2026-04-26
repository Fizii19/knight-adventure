using UnityEngine;
using System.Collections;

public class FinishTrigger : MonoBehaviour
{
    public AudioSource finishSound;
    public AudioClip finishSFX;
    public GameObject finishCanvas;
    public float delayTime = 6f;

    private Animator animChest;
    private bool isFinished = false;

    void Start()
    {
        animChest = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFinished) return;
        if (!collision.CompareTag("Player")) return;

        // ===== CEK KUNCI =====
        KeyManager km = FindFirstObjectByType<KeyManager>();
        if (km == null || !km.HasEnoughKeys())
        {
            Debug.Log("Kunci belum cukup!");
            return;
        }

        // ===== CEK ENEMY (LEVEL 3+) =====
        if (GameManager.instance != null && GameManager.instance.currentLevel >= 3)
        {
            if (GameManager.instance.enemiesKilled < GameManager.instance.enemiesRequired)
            {
                Debug.Log("Enemy belum cukup dibantai!");
                return;
            }
        }

        isFinished = true;

        // Stop BGM
        if (BGMManager.instance != null)
            BGMManager.instance.StopMusic();

        StartCoroutine(FadeOutBGM(0.5f));

        // Play SFX
        if (finishSound != null && finishSFX != null)
            finishSound.PlayOneShot(finishSFX);

        // Disable movement
        PlayerMovement pm = collision.GetComponent<PlayerMovement>();
        if (pm != null) pm.enabled = false;

        // Freeze player
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Force idle anim
        Animator anim = collision.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetFloat("Speed", 0f);
            anim.SetBool("isJumping", false);
        }

        // Chest anim
        if (animChest != null)
            animChest.SetTrigger("Finish");

        StartCoroutine(DelayFinish());
    }

    IEnumerator DelayFinish()
    {
        yield return new WaitForSeconds(delayTime);

        if (finishCanvas != null)
            finishCanvas.SetActive(true);
    }

    IEnumerator FadeOutBGM(float duration)
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