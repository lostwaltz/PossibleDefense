
//���������δ� Execute�� �Ⱦ��� ��쿡�� ��� ������ �ȵ�! - SOLID ��Ģ ���� �������� ġȯ ��Ģ ���� 
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
