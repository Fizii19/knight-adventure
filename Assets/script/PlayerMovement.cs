using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 20f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

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

    private float moveInput;
    private bool isGrounded;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        if (sfxSource == null)
            sfxSource = GetComponent<AudioSource>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (isDead) return;

        // =========================
        // MOVE INPUT
        // =========================

        float keyboardInput = Input.GetAxisRaw("Horizontal");

        moveInput = (keyboardInput != 0)
            ? keyboardInput
            : MobileInput.move;

        // Flip character
        if (moveInput > 0)
            sprite.flipX = false;
        else if (moveInput < 0)
            sprite.flipX = true;

        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        // =========================
        // JUMP
        // =========================

        bool jumpPressed =
            Input.GetKeyDown(KeyCode.Space) ||
            MobileInput.GetJump();

        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            isGrounded = false;

            anim.SetBool("isJumping", true);

            if (jumpSound != null)
                sfxSource.PlayOneShot(jumpSound);
        }

        // =========================
        // ATTACK
        // =========================

        bool attackPressed =
            Input.GetKeyDown(KeyCode.J) ||
            MobileInput.GetAttack();

        if (attackPressed)
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    public void Revive()
    {
        isDead = false;
        enabled = true;
    }

    // =========================
    // ATTACK
    // =========================

    void Attack()
    {
        anim.SetTrigger("Attack");

        if (attackSound != null)
            sfxSource.PlayOneShot(attackSound);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<SnailAI>()?.Knock();
            enemy.GetComponent<MushromAI>()?.TakeDamage(attackDamage);
        }
    }

    // =========================
    // GROUND CHECK
    // =========================

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

    // =========================
    // HURT
    // =========================

    public void PlayHurtAnimation()
    {
        anim.SetTrigger("Hurt");
    }

    // =========================
    // WATER DAMAGE
    // =========================

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            PlayerHealth hp = GetComponent<PlayerHealth>();

            if (hp != null)
            {
                hp.TakeDamage(hp.currentHealth);
            }
        }
    }

    // =========================
    // DEBUG ATTACK RANGE
    // =========================

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange
        );
    }
}