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
        //이러면 소환 될 때 당시의 enenmySO의 Modifier로 생성되겠지
        MaxHealth = enemy.baseMaxHP * enemy.maxHPModifier;
        curHealth = MaxHealth;
        HPBar.fillAmount = 1f;
        backHP.fillAmount = 1f;
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
            isDamaging = true;
            return false;
        }
    }

    private void HPDecrase()
    {
        //Hp는 즉각적으로
        HPBar.fillAmount = curHealth / MaxHealth;

        //backHP는 점차
        backHP.fillAmount = Mathf.Lerp(backHP.fillAmount, curHealth / MaxHealth, Time.deltaTime * decreaseSpeed);

        //backHP도 다 줄었으면 멈춤
        if (HPBar.fillAmount.Equals(backHP.fillAmount))
        {
            isDamaging = false;
        }
    }
}
