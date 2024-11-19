using UnityEngine;

[CreateAssetMenu(menuName = "AttackStrategy/AllEnemyAttackSO", fileName = "AllEnemyAttackSO" )]
public class AllEnemyAttackSO : AttackStrategySO
{
    public override IAttackStrategy GetAttackStrategy()
    {
        return new AllEnemyAttack();
    }
}