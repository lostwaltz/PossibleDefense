using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth {  get; private set; }
    public float curHealth { get; private set; }

    public void SetUp(EnemySO enemy)
    {
        //�̷��� ��ȯ �� �� ����� enenmySO�� Modifier�� �����ǰ���
        MaxHealth = enemy.baseMaxHP * enemy.maxHPModifier;
        curHealth = MaxHealth;
    }

    //������ true�� ��ȯ�ϴ� �Լ�
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
