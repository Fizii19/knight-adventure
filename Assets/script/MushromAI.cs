using System;
using UnityEngine;

public class MushromAI : MonoBehaviour
{
    public float chaseRange = 4f;
    public float moveSpeed = 2f;
    public int damage = 10;

    [Header("Health")]
    public int maxHealth = 20;
    private int currentHealth;
    private bool isDead = false;

    private PlayerHealth health; // ambil dari player otomatis
    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    private EnemyManager enemyManager;

    private bool isFacingRight = false;

    void Start()
    {
        // 🔍 Cari player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        enemyManager = FindFirstObjectByType<EnemyManager>();


        if (playerObj != null)
        {
            player = playerObj.transform;
            health = playerObj.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("Player tidak ditemukan! Pastikan tag 'Player' ada.");
        }

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Idle();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        anim.SetTrigger("Die");


        // Hentikan gerakan
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Matikan collider
        GetComponent<Collider2D>().enabled = false;
        if (enemyManager != null)
        {
            enemyManager.EnemyDied();
        }
        // Hapus setelah animasi
        Destroy(gameObject, 2f);
    }

    void ChasePlayer()
    {
        anim.SetBool("isRunning", true);

        // Flip arah
        if (player.position.x > transform.position.x && !isFacingRight)
            Flip();
        else if (player.position.x < transform.position.x && isFacingRight)
            Flip();

        // Kalau player mati → stop
        if (health != null && health.isDead)
        {
            anim.SetBool("isRunning", false);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }

        // Gerak ke player
        Vector2 target = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );
    }

    void Idle()
    {
        anim.SetBool("isRunning", false);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider
                .GetComponent<PlayerHealth>()?
                .TakeDamage(damage);
        }
    }
}