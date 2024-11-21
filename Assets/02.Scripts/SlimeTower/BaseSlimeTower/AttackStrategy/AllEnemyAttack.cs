using UnityEngine;

public class AllEnemyAttack : IAttackStrategy
{
    private float _damage;
    private Vector3 _offset = new Vector3(15f, 10f, -12f);

    public void Execute(Transform target)
    {
        GameObject allAttackParticle = PoolManagerForTest.Instance.poolLegacy.SpawnFromPool("AllAttackParticle");
        DamageParticle damageParticle = allAttackParticle.GetComponent<DamageParticle>();
        damageParticle.Setting(_offset, _damage);
        damageParticle.StartParticleLifeCycle();
    }


    public void Setting(Transform firePos, Transform targetPos, float damage)
    {
        _damage = damage;
    }
}