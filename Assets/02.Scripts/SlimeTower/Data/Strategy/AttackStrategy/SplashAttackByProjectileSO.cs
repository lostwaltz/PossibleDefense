using UnityEngine;

[CreateAssetMenu(menuName = "AttackStrategy/SplashAttackByProjectileSO", fileName = "SplashAttackByProjectileSO")]
public class SplashAttackByProjectileSO : AttackStrategySO
{
    public override IAttackStrategy GetAttackStrategy()
    {
        return new SplashAttackByProjectile();
    }
}


