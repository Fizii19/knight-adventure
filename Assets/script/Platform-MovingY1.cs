using UnityEngine;

public class MovingPlatformY2 : MonoBehaviour
{
    public float speed = 1f;
    public float moveDistance = 6f;

    private Vector3 startPos;
    private int direction = -1; // Mulai turun dulu
    private Rigidbody2D rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        Vector2 movement = Vector2.up * direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Batas atas
        if (rb.position.y > startPos.y + moveDistance)
            direction = -1;

        // Batas bawah
        if (rb.position.y < startPos.y - moveDistance)
            direction = 1;
    }
}
