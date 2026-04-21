using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1f;        // Kecepatan platform
    public float moveDistance = 6f; // Jarak kanan-kiri
    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Gerakkan platform
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // Jika sudah mencapai batas kanan
        if (transform.position.x > startPos.x + moveDistance)
            direction = -1;

        // Jika mencapai batas kiri
        if (transform.position.x < startPos.x - moveDistance)
            direction = 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DetachNextFrame(collision.transform));
        }
    }

    private IEnumerator DetachNextFrame(Transform target)
    {
        yield return null; // Tunggu 1 frame → mencegah error SetParent
        target.SetParent(null);
    }
}
