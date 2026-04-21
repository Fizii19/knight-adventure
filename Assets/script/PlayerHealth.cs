using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 20;
    public int currentHealth;
    public Animator animator;

    public PlayerMovement movement;
    public bool isDead = false;

    public AudioSource audioSource;
    public AudioClip dieSound;

    public GameOverManager gameOverManager;

    void Start()
    {
        currentHealth = maxHealth;
        movement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("HP Player: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();

            Debug.Log("player mati");

            // Stop BGM
            BGMManager.instance?.StopMusic();

            // Play die sound
            if (audioSource != null && dieSound != null)
                audioSource.PlayOneShot(dieSound);
        }
    }

    public void Revive()
    {
        isDead = false;
        currentHealth = maxHealth;
        movement.enabled = true;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;

        // Reset animasi kalau ada
        if (animator != null)
            animator.ResetTrigger("Die");

        // Hidupkan movement lagi
        if (movement != null)
            movement.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            // isi kalau butuh
        }
    }

    void Die()
    {
        isDead = true;
        currentHealth = 0;

        // Animasi mati
        animator.SetTrigger("Die");

        // Disable movement
        movement.enabled = false;

        // Delay GameOver
        StartCoroutine(ShowGameOverAfterDelay());
    }

    private IEnumerator ShowGameOverAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (gameOverManager != null)
            gameOverManager.ShowGameOver();
    }
}
