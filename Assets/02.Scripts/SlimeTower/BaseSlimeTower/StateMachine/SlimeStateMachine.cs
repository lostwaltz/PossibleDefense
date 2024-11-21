using UnityEngine;

public class SlimeStateMachine : BaseStateMachine
{
    public BaseSlimeTower SlimeTower { get; private set; }
    public SlimeTowerIdleState IdleState { get; private set; }
    public SlimeTowerAttackState AttackState { get; private set; }

    public SlimeTowerWalkState WalkState { get; private set; }

 
    public SlimeStateMachine(BaseSlimeTower slimeTower)
    {
        IdleState = new SlimeTowerIdleState(this);
        AttackState = new SlimeTowerAttackState(this);
        WalkState = new SlimeTowerWalkState(this);
        SlimeTower = slimeTower;
    }
}