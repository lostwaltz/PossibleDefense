using UnityEngine;

public class SlowDebuffAndAttack : IAttackStrategy
{
    private IFireStrategy _fireStrategy;
    private IHitStrategy _hitStrategy;
    private Transform _firePos;
    private float _damage;

    public void Setting(Transform firePos, Transform targetPos, float damage)
    {
        _firePos = firePos;
        _damage = damage;
        _fireStrategy = new FireAtTarget();
    }

    public void Execute(Transform target)
    {
        GameObject projectile = PoolManagerForTest.Instance.poolLegacy.SpawnFromPool("Bullet");
        projectile.transform.position = _firePos.position;
        _hitStrategy = new DebuffHitStrategy(projectile.transform, DebuffType.Slow);
        projectile.GetComponent<BaseProjectile>().SetProjectile(_fireStrategy, _hitStrategy, target, _damage);
    }
}