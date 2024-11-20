
//정석적으로는 Execute를 안쓰는 경우에는 상속 받으면 안됨! - SOLID 원칙 위배 리스코프 치환 원칙 위배 
using UnityEngine;
public class BasicHitStrategy : IHitStrategy
{    
    private Transform _projectilePos;

    public BasicHitStrategy( Transform projectilePos)
    {
        _projectilePos = projectilePos;
    }

    public void Execute()
    {
        GameObject particle = PoolManagerForTest.Instance.Pool.SpawnFromPool("HitParticle");
        BaseParticle baseParticle = particle.GetComponent<BaseParticle>();
        baseParticle.Setting(_projectilePos);
        baseParticle.StartParticleLifeCycle();
    }
    
}
