using UnityEngine;


//TODO 오브젝트 풀에서 꺼내 오기 
public class SingleTargetAttackByProjectile : IAttackStrategy
{

    private IFireStrategy _fireStrategy;
    private IHitStrategy _hitStrategy;
    private Transform _firePos;
    private float _damage;
    


    public void Setting(Transform firePos , Transform targetPos , float damage)
    {
        _firePos = firePos;
        _damage = damage;
        _fireStrategy = new FireAtTarget();
        _hitStrategy  = new BasicHitStrategy(_damage);
    }

    public void Execute(Transform  target)
    { 
        GameObject projectile = PoolManagerForTest.Instance.Pool.SpawnFromPool("Bullet");
        projectile.transform.position = _firePos.position;
        projectile.GetComponent<BaseProjectile>().SetProjectile(_fireStrategy,_hitStrategy,target,_damage);
    }
}