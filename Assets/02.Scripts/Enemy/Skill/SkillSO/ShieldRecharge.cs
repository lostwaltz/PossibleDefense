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

                
                if(SpawnManager.Instance.ObjectPoolLegacy.SpawnFromPool("Shield").TryGetComponent<BaseParticle>(out BaseParticle particle))
                    particle.Setting(slime.transform);
            }
        }
    }
}
