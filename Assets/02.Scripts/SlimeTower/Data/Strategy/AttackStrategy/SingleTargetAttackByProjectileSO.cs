using UnityEngine;

[CreateAssetMenu(menuName = "AttackStrategy/SingleTargetAttackByProjectile", fileName = "SingleTargetAttackByProjectileSO" )]
public class SingleTargetAttackByProjectileSO : AttackStrategySO
{
    public override IAttackStrategy GetAttackStrategy()
    {
        return new SingleTargetAttackByProjectile();
    }
}