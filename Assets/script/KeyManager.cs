using TMPro;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public int keyCount = 0;
    public int requiredKeys = 3; // set beda tiap level

    public TextMeshProUGUI keyText;

    void Start()
    {
        UpdateUI();
    }

    public void AddKey()
    {
        keyCount++;
        UpdateUI();
    }

    void UpdateUI()
    {
        keyText.text = keyCount + " / " + requiredKeys;
    }

    public bool HasEnoughKeys()
    {
        return keyCount >= requiredKeys;
    }
}