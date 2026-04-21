using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 20f;

    [Header("Attack Settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayers;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip jumpSound;
    public AudioClip attackSound;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    float moveInput;
    private bool isGrounded;
    private bool isDead = false;

    void Start()
    {
        this.enabled = true ;
        sfxSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (isDead) return;

        // ===== MOVEMENT =====
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0) sprite.flipX = false;
        else if (moveInput < 0) sprite.flipX = true;

        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        // ===== JUMP =====
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetBool("isJumping", true);
            sfxSource.PlayOneShot(jumpSound);
        }

        // ===== ATTACK =====
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    public void Revive()
    {
        isDead = false ;
        this.enabled = true;
    }

    // ===== ATTACK =====
    void Attack()
    {
        anim.SetTrigger("Attack");

        // MAINKAN SUARA ATTACK
        sfxSource.PlayOneShot(attackSound);

        // STOP SUARA SETELAH ANIMASI SELESAI (opsional)
        Invoke(nameof(StopAttackAudio), 0.3f);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>()?.TakeDamage(attackDamage);
        }
    }

    void StopAttackAudio()
    {
        sfxSource.Stop();
    }

    // ===== GROUND CHECK =====
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void PlayHurtAnimation()
    {
        anim.SetTrigger("Hurt");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            PlayerHealth hp = GetComponent<PlayerHealth>();

            if (hp != null)
            {
                hp.TakeDamage(hp.currentHealth); // bikin hp langsung habis
            }
        }
    }
}
