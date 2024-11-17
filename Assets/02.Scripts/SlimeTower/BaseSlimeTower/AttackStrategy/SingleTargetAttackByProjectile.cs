using UnityEngine;


//TODO 오브젝트 풀에서 꺼내 오기 
public class SingleTargetAttackByProjectile : IAttackStrategy
{

    private IFireStrategy _fireStrategy;
    private IHitStrategy _hitStrategy;
    private Transform _targetPos;
    private Vector3 _firePos;
    private float _damage;

    
    
     public SingleTargetAttackByProjectile(Vector3 firePos , Transform targetPos , float damage)
    {
        _targetPos = targetPos;
        _firePos = firePos;
        _damage = damage;

        _fireStrategy = new FireAtTarget();
        _hitStrategy  = new BasicHitStrategy(_damage);
    }
    
    
    public void Execute()
    { 
        GameObject projectile = PoolManagerForTest.Instance.Pool.SpawnFromPool("Bullet");
        projectile.transform.position = _firePos;
        projectile.GetComponent<BaseProjectile>().SetProjectile(_fireStrategy,_hitStrategy,_targetPos);
     }
}