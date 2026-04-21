using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material mat;
    private float distance;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;

    public Transform player; // referensi ke player
    private Vector3 lastPlayerPos;

    void Start()
    {
        mat = GetComponent<Renderer>().material;

        if (player != null)
            lastPlayerPos = player.position;
    }

    void Update()
    {
        if (player != null)
        {
            // hitung seberapa jauh player bergerak horizontal
            float deltaX = player.position.x - lastPlayerPos.x;

            // hanya geser texture jika player bergerak
            if (Mathf.Abs(deltaX) > 0.001f)
            {
                distance += deltaX * speed;
                mat.SetTextureOffset("_MainTex", new Vector2(distance, 0));
            }

            lastPlayerPos = player.position;
        }
    }
}
