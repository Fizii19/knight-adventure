using UnityEngine;

public class MovingPlatformY : MonoBehaviour
{
    public float speed = 1f;
    public float moveDistance = 6f;


    private Vector3 startPos;
    private int direction = 1;
    private Rigidbody2D rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();

        // FIX di sini
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void FixedUpdate()
    {
        // Gerakan naik-turun
        Vector2 movement = Vector2.up * direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Batas atas
        if (transform.position.y > startPos.y + moveDistance)
            direction = -1;

        // Batas bawah
        if (transform.position.y < startPos.y - moveDistance)
            direction = 1;
    }
}
