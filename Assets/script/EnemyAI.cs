using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float chaseRange = 4f;
    public float moveSpeed = 2f;
    public int damage = 10;

    // 📌 Tambahan
    public int maxHealth = 20;
    private int currentHealth;
    private bool isDead = false;

    public PlayerHealth health;

    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    private bool isFacingRight = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;        // initialize HP
    }

    void Update()
    {
        if (isDead) return; // musuh mati = stop AI

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
        
        if (GameManager.instance != null)
        {
            GameManager.instance.EnemyKilled();
        }

        // Hentikan fisika
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Opsional: matikan collider
        GetComponent<Collider2D>().enabled = false;

        // 🔥 Hilangkan enemy setelah 2 detik (selesai animasi mati)
        Destroy(gameObject, 2f);
    }


    void ChasePlayer()
    {
        anim.SetBool("isRunning", true);
        if (player.position.x > transform.position.x && !isFacingRight)
            Flip();
        else if (player.position.x < transform.position.x && isFacingRight)
            Flip();
        if (health.isDead)
        {
            anim.SetBool("isRunning", false);
            rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
        Vector2 target = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        
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
            collision.collider.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
}
