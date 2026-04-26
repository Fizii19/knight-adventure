using UnityEngine;
using System.Collections;

public class SnailAI : MonoBehaviour
{
    public float speed = 2f;
    public float moveTime = 3f;
    public float idleTime = 2f;
    public float knockTime = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform wallCheck;
    public float wallDistance = 0.2f;

    private bool isKnocked = false;
    private Rigidbody2D rb;
    private Animator anim;

    private Coroutine moveRoutine;
    private int direction = 1; // 1 kanan, -1 kiri

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        moveRoutine = StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return Move();
            yield return Idle();
        }
    }

    IEnumerator Move()
    {
        float timer = 0f;

        anim.SetBool("isWalking", true);
        UpdateDirectionVisual();

        while (timer < moveTime)
        {
            // kalau ga napak tanah, diem
            if (!IsGrounded())
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                yield return null;
                continue;
            }

            // ketemu jurang → balik
            if (!IsGroundAhead())
            {
                Flip();
                yield return null;
                continue;
            }

            // ketemu tembok → balik
            if (IsWallAhead())
            {
                Flip();
                yield return null;
                continue;
            }

            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

            timer += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isWalking", false);
    }

    IEnumerator Idle()
    {
        float timer = 0f;

        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isWalking", false);

        while (timer < idleTime)
        {
            yield return null;
            timer += Time.deltaTime;
        }
    }

    void Flip()
    {
        direction *= -1;
        UpdateDirectionVisual();
    }

    void UpdateDirectionVisual()
    {
        transform.localScale = new Vector3(direction > 0 ? -1 : 1, 1, 1);
    }

    public void Knock()
    {
        if (!isKnocked)
            StartCoroutine(KnockRoutine());
    }

    IEnumerator KnockRoutine()
    {
        isKnocked = true;

        if (moveRoutine != null) StopCoroutine(moveRoutine);
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        anim.SetTrigger("isHit");
        anim.SetBool("isKnocked", true);
        anim.SetBool("isWalking", false);

        yield return new WaitForSeconds(knockTime);

        anim.SetBool("isKnocked", false);
        isKnocked = false;

        moveRoutine = StartCoroutine(MoveRoutine());
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundDistance,
            groundLayer
        );
    }

    bool IsGroundAhead()
    {
        Vector2 checkPos = (Vector2)transform.position + new Vector2(direction * 0.5f, 0);

        return Physics2D.Raycast(
            checkPos,
            Vector2.down,
            groundDistance,
            groundLayer
        );
    }

    bool IsWallAhead()
    {
        return Physics2D.Raycast(
            wallCheck.position,
            new Vector2(direction, 0),
            wallDistance,
            groundLayer
        );
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isKnocked) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth hp = collision.gameObject.GetComponent<PlayerHealth>();

            if (hp != null)
            {
                hp.TakeDamage(10);
            }
        }
    }
}