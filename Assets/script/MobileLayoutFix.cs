using UnityEngine;

public class MobileLayoutFix : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform leftControls;
    public RectTransform rightControls;

    [Header("Ultra Wide Layout Settings (Ratio >= 2.2)")]
    [Tooltip("Untuk HP layar sangat panjang/lebar/lipat")]
    public Vector2 ultraLeftPos;
    public Vector2 ultraRightPos;

    [Header("Normal Mobile Layout Settings (Ratio 1.8 - 2.19)")]
    [Tooltip("Untuk HP standar zaman sekarang (misal: Redmi Note 7)")]
    public Vector2 normalLeftPos;
    public Vector2 normalRightPos;

    [Header("Desktop Layout Settings (Ratio 1.5 - 1.79)")]
    [Tooltip("Untuk monitor PC / Laptop standar (16:9)")]
    public Vector2 desktopLeftPos;
    public Vector2 desktopRightPos;

    [Header("Tablet Layout Settings (Ratio < 1.5)")]
    [Tooltip("Untuk iPad atau Tablet Android yang layarnya cenderung lebih kotak (4:3)")]
    public Vector2 tabletLeftPos;
    public Vector2 tabletRightPos;

    void Start()
    {
        // Menghitung aspect ratio layar saat ini
        float ratio = (float)Screen.width / Screen.height;

        Debug.Log("<color=yellow>MobileLayoutFix -> Aspect Ratio Aktif: </color>" + ratio);

        // 1. Cek Kategori Ultra Wide
        if (ratio >= 2.2f)
        {
            Debug.Log("<color=cyan>MobileLayoutFix -> Menggunakan: Ultra Wide Layout</color>");
            leftControls.anchoredPosition = ultraLeftPos;
            rightControls.anchoredPosition = ultraRightPos;
        }
        // 2. Cek Kategori HP Normal (Di bawah 2.2 tapi di atas/sama dengan 1.8)
        else if (ratio >= 1.8f)
        {
            Debug.Log("<color=green>MobileLayoutFix -> Menggunakan: Normal Layout</color>");
            leftControls.anchoredPosition = normalLeftPos;
            rightControls.anchoredPosition = normalRightPos;
        }
        // 3. Cek Kategori Desktop (Di bawah 1.8 tapi di atas/sama dengan 1.5)
        else if (ratio >= 1.5f)
        {
            Debug.Log("<color=orange>MobileLayoutFix -> Menggunakan: Desktop Layout</color>");
            leftControls.anchoredPosition = desktopLeftPos;
            rightControls.anchoredPosition = desktopRightPos;
        }
        // 4. Sisanya masuk Kategori Tablet (Di bawah 1.5)
        else
        {
            Debug.Log("<color=magenta>MobileLayoutFix -> Menggunakan: Tablet Layout</color>");
            leftControls.anchoredPosition = tabletLeftPos;
            rightControls.anchoredPosition = tabletRightPos;
        }
    }
}