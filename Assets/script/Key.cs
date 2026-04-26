using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public int keyValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerKey playerKey = collision.GetComponent<PlayerKey>();
            if (playerKey != null)
            {
                FindFirstObjectByType<KeyManager>().AddKey();
                playerKey.AddKey(keyValue);
                Destroy(gameObject);
            }
        }
    }
}
