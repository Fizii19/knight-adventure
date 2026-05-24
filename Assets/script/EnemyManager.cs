using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int totalEnemies;
    private int deadEnemies;

    [Header("UI")]
    public TextMeshProUGUI enemyText;

    void Start()
    {
        UpdateUI();
    }

    public void RegisterEnemy()
    {
        totalEnemies++;
        UpdateUI();
    }

    public void EnemyDied()
    {
        deadEnemies++;

        // supaya tidak lebih dari total
        deadEnemies = Mathf.Clamp(deadEnemies, 0, totalEnemies);

        UpdateUI();
    }

    void UpdateUI()
    {
        if (enemyText != null)
        {
            enemyText.text = deadEnemies + " / " + totalEnemies;
        }
    }

    public bool AllEnemiesDead()
    {
        return deadEnemies >= totalEnemies;
    }
}