using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("ShieldRecharge"), menuName = ("Enemy/Skill/ShieldRecharge"))]

public class ShieldRecharge : BaseSkillSO
{
    public float shieldAmount;

    public override void Execute(Enemy enemy)
    {
        Collider[] nearSlimes = Physics.OverlapSphere(enemy.transform.position, radius, TargetLayer);

        foreach(Collider slime in nearSlimes)
        {
            if(slime.TryGetComponent<EnemyHealth>(out  EnemyHealth enemyHealth))
            {
                enemyHealth.ShieldRecharge(shieldAmount);

                //buff effect's duration 3sec
                if(SpawnManager.Instance.ObjectPool.SpawnFromPool("Shield").TryGetComponent<ParticleController>(out ParticleController particle))
                    particle.Initialize(slime.transform, 3f);   
            }
        }
    }
}
