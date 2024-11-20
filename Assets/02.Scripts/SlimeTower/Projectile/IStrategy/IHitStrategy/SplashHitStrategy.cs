using UnityEngine;

public class SplashHitStrategy : IHitStrategy
{
    private Transform _projectilePos;
    private float _damage;
    private Collider[] colls = new Collider[10];
    private int _targetLayerMask = LayerMask.GetMask("Enemy");


    public SplashHitStrategy(float damage, Transform projectilePos)
    {
        _projectilePos = projectilePos;
        _damage = damage;
    }


    
    //TODO 파티클 생성 위치를 변경해야함. ENEMY딴에서 하는게 더 좋아 보임
    public void Execute()
    {
        
         int count = Physics.OverlapSphereNonAlloc(_projectilePos.position, 5f, colls);

        for (int i = 0; i < count; i++)
        {
            var collider = colls[i];

            if ((_targetLayerMask & (1 << collider.gameObject.layer)) == 0) continue;

            var enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
                GameObject particle = PoolManagerForTest.Instance.Pool.SpawnFromPool("SplashParticle");
                DamageParticle damageParticle = particle.GetComponent<DamageParticle>();
                damageParticle.Setting(_projectilePos,_damage);
                damageParticle.StartParticleLifeCycle();
            }
        }
    }

 
}