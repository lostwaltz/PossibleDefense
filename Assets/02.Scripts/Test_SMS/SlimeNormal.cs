using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeNormal : MonoBehaviour
{
    public Bullet BulletPrefeb;//���Ÿ� ����ü
    public TargetSearch TargetSearch;//������ �� ��ü Ž��
    public float AttackCoolTime; //���� ��Ÿ��
    public float curAttackTime; //���� ��Ÿ��
    private void Awake()
    {
        curAttackTime = AttackCoolTime;
        if (TargetSearch == null)
        {
            TargetSearch = GetComponent<TargetSearch>();
        }
    }

    private void Update()
    {
        if(AttackCoolTime <= curAttackTime)
        {
            if (TargetSearch.ShortEnemyTarget != null)
            {
                curAttackTime = 0f;
                Bullet Bullet = Instantiate(BulletPrefeb);
                Bullet.transform.position = transform.position;
                Bullet.Initialize(TargetSearch.TargetToDirection());
            }
        }
        else
        {
            curAttackTime += Time.deltaTime;
        }
    }
}
