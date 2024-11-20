public class SlowDebuffSingleTargetAttackSO  : AttackStrategySO
{
    public override IAttackStrategy GetAttackStrategy()
    {
        return new SlowDebuffSingleTargetAttack();
    }
}