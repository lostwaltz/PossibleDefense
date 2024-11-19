using UnityEngine;

public class Health
{
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public void Initialize(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
    }

    public float TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        return CurrentHealth;
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
    }

    public bool IsDead => CurrentHealth <= 0;
}

