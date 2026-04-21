using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    [Header("Assign camera manual")]
    public Camera targetCamera;   // assign via Inspector

    [Header("Offset tambahan (opsional)")]
    public Vector3 offset = Vector3.zero;

    void LateUpdate()
    {
        if (targetCamera == null)
            return;

        // ikuti posisi kamera
        Vector3 camPos = targetCamera.transform.position;

        // pastikan background tetap pada depth-nya (misalnya z = 10)
        transform.position = new Vector3(
            camPos.x + offset.x,
            camPos.y + offset.y,
            offset.z   // <-- Z tetap, tidak berubah
        );

    }
}
