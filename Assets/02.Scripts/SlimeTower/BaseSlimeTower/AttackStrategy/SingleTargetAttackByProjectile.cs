using UnityEngine;

public class SingleTargetAttackByProjectile : IAttackStrategy
{

    private IFireStrategy _fireStrategy;
    private IHitStrategy _hitStrategy;
    private Transform _firePos;
    private float _damage;
    private Transform _targetPos;


    public void Setting(Transform firePos , Transform targetPos , float damage)
    {
        _firePos = firePos;
        _damage = damage;
        _targetPos = targetPos;
        _fireStrategy = new FireAtTarget();
    }

    
    public void Execute(Transform  target)
    { 
        GameObject projectile = PoolManagerForTest.Instance.poolLegacy.SpawnFromPool("Bullet");
        projectile.transform.position = _firePos.position;
        _hitStrategy  = new BasicHitStrategy(projectile.transform );
        projectile.GetComponent<BaseProjectile>().SetProjectile(_fireStrategy,_hitStrategy,target,_damage);
    }
}