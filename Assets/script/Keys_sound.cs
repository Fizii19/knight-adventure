using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX(pickupSound);
            Destroy(gameObject);
        }
    }
}