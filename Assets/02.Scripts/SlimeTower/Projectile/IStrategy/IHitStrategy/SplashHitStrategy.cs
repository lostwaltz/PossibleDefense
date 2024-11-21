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
        GameObject particle = PoolManagerForTest.Instance.poolLegacy.SpawnFromPool("SplashParticle");
        DamageParticle damageParticle = particle.GetComponent<DamageParticle>();
        Vector3 offset = Vector3.up * 3f;
        damageParticle.Setting(_projectilePos, offset, _damage);
        damageParticle.StartParticleLifeCycle();
    }
}