using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FitMeshBackground : MonoBehaviour
{
    void Start()
    {
        FitToScreen();
    }

    void FitToScreen()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

        // ukuran asli mesh (belum kena scale)
        Vector3 meshSize = mesh.bounds.size;

        // ukuran world camera
        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * cam.aspect;

        // hitung scale
        float scaleX = worldWidth / meshSize.x;
        float scaleY = worldHeight / meshSize.y;

        // supaya full screen
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}