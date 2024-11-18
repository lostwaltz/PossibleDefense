using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth {  get; private set; }
    public float curHealth { get; private set; }

    private float maxShield;
    private float curShield;
    private float evasion;

    [SerializeField] private Image HPBar;
    [SerializeField] private Image backHP;
    [SerializeField] private Image shield;

    private float decreaseSpeed = 2;
    private bool isDamaging = false;
    private bool hasShield = false;

    private void Update()
    {
        if (isDamaging)
        {
            HPDecrase();
        }
    }


    public void SetUp(EnemySO enemy)
    {
        MaxHealth = enemy.baseMaxHP * enemy.maxHPModifier;
        curHealth = MaxHealth;

        maxShield = enemy.shield;
        curShield = maxShield;

        evasion = enemy.evasion;

        HPBar.fillAmount = 1f;
        backHP.fillAmount = 1f;

        hasShield = maxShield > 0;
        shield.fillAmount = hasShield ? 1f : 0f;
    }

    public int TakeDamage(float damage)
    {
        float evasionPecentage = Random.Range(0, 100f);
        if (evasionPecentage < evasion) return -1;

        float remainingDamage = HandleShield(damage);
        if (remainingDamage < 0)
        {
            return -1;  //only shield damaged
        }

        return HandleHP(remainingDamage);
    }

    private float HandleShield(float damage)
    {
        if (hasShield)
        {
            curShield -= damage;

            //shield crashed
            if (curShield < 0f)
            {
                float remainingDamage = -curShield;
                curShield = 0f;
                hasShield = false;
                shield.fillAmount = 0f;
                return remainingDamage;
            }

            //remain shield
            shield.fillAmount = curShield / maxShield;
            return -1;
        }

        //don't have shield
        return damage;
    }

    private int HandleHP(float damage)
    {

        if (damage > 0f)
        {
            curHealth = Mathf.Clamp(curHealth - damage, 0f, MaxHealth);
            HPBar.fillAmount = curHealth / MaxHealth;
        }

        if (curHealth <= 0f)
        {
            return 0;   //dead
        }
        else
        {
            isDamaging = true;
            return 1;   //damaged
        }
    }

    private void HPDecrase()
    {
        backHP.fillAmount = Mathf.Lerp(backHP.fillAmount, curHealth / MaxHealth, Time.deltaTime * decreaseSpeed);

        if (HPBar.fillAmount.Equals(backHP.fillAmount))
        {
            isDamaging = false;
        }
    }
}
