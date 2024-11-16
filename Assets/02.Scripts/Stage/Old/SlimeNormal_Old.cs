using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeNormal : MonoBehaviour
{
    public Bullet BulletPrefeb;//원거리 투사체
    public TargetSearch TargetSearch;//공격할 적 객체 탐색
    public float AttackCoolTime; //공격 쿨타임
    public float curAttackTime; //공격 쿨타임
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
