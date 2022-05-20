using UnityEngine;
using Unity.Collections;

public class HealthSO : ScriptableObject
{
    [SerializeField] [ReadOnly] private int maxHealth;
    [SerializeField] [ReadOnly] private int currentHealth;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public void SetMaxHealth(int value) => maxHealth = value;

    public void SetCurrentHealth(int value) => currentHealth = value;

    public void IncreaseHealth(int value)
    {
        if (currentHealth + value >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += value;
        }
    }

    public void DecreaseHealth(int value)
    {
        if (currentHealth - value <= 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= value;
        }
    }
}
