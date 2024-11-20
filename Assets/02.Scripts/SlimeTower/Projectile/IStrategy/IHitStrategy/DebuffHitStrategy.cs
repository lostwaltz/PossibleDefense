public enum DebuffType
{
    Slow, // 느려지게 하는 디버프
    AttackReduction // 공격력이 약해지게 하는 디버프
}

public class DebuffHitStrategy : IHitStrategy
{
    private float slowAmount;
    private DebuffType _type;

    public DebuffHitStrategy(float slowAmount , DebuffType type)
    {
        this.slowAmount = slowAmount;
    }


    public void Execute()
    {
        // 디버프 파티클을 생성해서 적용하기 
    }
    
}