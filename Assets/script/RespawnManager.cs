using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;

    [Header("Respawn Settings")]
    public Transform respawnPoint;     // tempat respawn player
    public float respawnDelay = 1.5f;  // delay sebelum respawn

    private GameObject player;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        player.SetActive(false);              // sembunyikan player
        yield return new WaitForSeconds(respawnDelay);

        // Reset posisi
        player.transform.position = respawnPoint.position;

        // Reset health
        PlayerHealth hp = player.GetComponent<PlayerHealth>();
        hp.ResetHealth();

        // Nyalakan player kembali
        player.SetActive(true);
    }
}
