using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth {  get; private set; }
    public float curHealth { get; private set; }

    public void SetUp(EnemySO enemy)
    {
        //이러면 소환 될 때 당시의 enenmySO의 Modifier로 생성되겠지
        MaxHealth = enemy.baseMaxHP * enemy.maxHPModifier;
        curHealth = MaxHealth;
    }

    //죽을때 true를 반환하는 함수
    public bool TakeDamage(float damage)
    {
        curHealth = Mathf.Clamp(curHealth -= damage, 0f, MaxHealth);

        if(curHealth <= 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
