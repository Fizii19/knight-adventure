using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    public int keyCount = 0;

    public void AddKey(int amount)
    {
        keyCount += amount;
        Debug.Log("Key sekarang: " + keyCount);
    }

    public bool HasKey()
    {
        return keyCount > 0;
    }

    public void UseKey()
    {
        if (keyCount > 0)
            keyCount--;
    }
}
