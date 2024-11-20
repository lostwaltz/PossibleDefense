using UnityEngine;

public class Shield
{
    public float MaxShield { get; private set; }
    public float CurrentShield { get; private set; }

    public void Initialize(float maxShield)
    {
        MaxShield = maxShield;
        CurrentShield = MaxShield;
    }

    public float AbsorbDamage(float damage)
    {
        float remainingDamage = damage - CurrentShield;
        CurrentShield = Mathf.Clamp(CurrentShield - damage, 0, MaxShield);
        return remainingDamage > 0 ? remainingDamage : 0;
    }

    public void Recharge(float amount)
    {
        if(MaxShield < CurrentShield + amount)
        {
            MaxShield = amount;
        }

        CurrentShield = Mathf.Clamp(CurrentShield + amount, 0, MaxShield);
    }

    public float GetShieldPercentage => CurrentShield / MaxShield;

}

