using UnityEngine;

[CreateAssetMenu(menuName = "AttackStrategy/SlowDebuffAndAttackSO", fileName = "SlowDebuffAndAttackSO" )]

public class SlowDebuffAndAttackSO : AttackStrategySO
{
    public override IAttackStrategy GetAttackStrategy()
    {
        return new SlowDebuffAndAttack();
    }
}