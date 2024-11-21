using System.Text;
using UnityEngine;

public enum DebuffType
{
    Slow, // 느려지게 하는 디버프
    AttackReduction // 공격력이 약해지게 하는 디버프
}

public class DebuffHitStrategy : IHitStrategy
{
   // private float slowAmount; 지금은 파티클에서 설정 해당 부분 관련해서 기획적으로 고민
    private Transform _projectilePos;
    private DebuffType _type;

    private StringBuilder _stringBuilder = new StringBuilder("Particle");

    public DebuffHitStrategy(Transform projectilePos,DebuffType type)
    {
        _projectilePos = projectilePos;
        _type = type;
    }


    public void Execute()
    {
        GameObject debuffParticle =
            PoolManagerForTest.Instance.poolLegacy.SpawnFromPool(_stringBuilder.Insert(0, _type).ToString());
        ExecuteParticle executeParticle = debuffParticle.GetComponent<ExecuteParticle>();
        Vector3 offset = Vector3.up * 3f;
        executeParticle.Setting(_projectilePos, offset);
        executeParticle.StartParticleLifeCycle();
    }
}