using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth {  get; private set; }
    public float curHealth { get; private set; }

    [SerializeField] private Image HPBar;
    [SerializeField] private Image backHP;

    private float decreaseSpeed = 2;
    private bool isDamaging = false;

    private void Update()
    {
        if (isDamaging)
        {
            HPDecrase();
        }
    }


    public void SetUp(EnemySO enemy)
    {
        //�̷��� ��ȯ �� �� ����� enenmySO�� Modifier�� �����ǰ���
        MaxHealth = enemy.baseMaxHP * enemy.maxHPModifier;
        curHealth = MaxHealth;
        HPBar.fillAmount = 1f;
        backHP.fillAmount = 1f;
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
            isDamaging = true;
            return false;
        }
    }

    private void HPDecrase()
    {
        //Hp�� �ﰢ������
        HPBar.fillAmount = curHealth / MaxHealth;

        //backHP�� ����
        backHP.fillAmount = Mathf.Lerp(backHP.fillAmount, curHealth / MaxHealth, Time.deltaTime * decreaseSpeed);

        //backHP�� �� �پ����� ����
        if (HPBar.fillAmount.Equals(backHP.fillAmount))
        {
            isDamaging = false;
        }
    }
}
