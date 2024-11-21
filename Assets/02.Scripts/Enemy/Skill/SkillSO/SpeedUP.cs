using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("SpeedUP"), menuName = ("Enemy/Skill/SpeedUP"))]
public class SpeedUP : BaseSkillSO
{
    public float percentage;
    public float duration;

    public override void Execute(Enemy enemy)
    {
        Collider[] nearSlimes = Physics.OverlapSphere(enemy.transform.position, radius, TargetLayer);

        foreach (Collider slime in nearSlimes)
        {
            if(slime.TryGetComponent<ForceReceiver>(out ForceReceiver force))
            {
                force.SpeedBuff(percentage, duration, true);
                if (SpawnManager.Instance.ObjectPoolLegacy.SpawnFromPool("Speed").TryGetComponent<BaseParticle>(out BaseParticle particle))
                {
                    particle.Setting(slime.transform);
                }
            }
        }
    }
}
