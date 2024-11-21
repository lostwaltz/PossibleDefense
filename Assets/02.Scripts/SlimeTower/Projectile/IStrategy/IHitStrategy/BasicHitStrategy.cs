

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
        GameObject particle = PoolManagerForTest.Instance.poolLegacy.SpawnFromPool("HitParticle");
        BaseParticle baseParticle = particle.GetComponent<BaseParticle>();
        Vector3 offset = Vector3.up * 3f;
        baseParticle.Setting(_projectilePos,offset);
        baseParticle.StartParticleLifeCycle();
    }
    
}

