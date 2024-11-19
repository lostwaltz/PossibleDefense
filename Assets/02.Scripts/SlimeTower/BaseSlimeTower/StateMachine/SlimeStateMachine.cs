using UnityEngine;

public class SlimeStateMachine : BaseStateMachine
{
    public BaseSlimeTower SlimeTower { get; private set; }
    public SlimeTowerIdleState IdleState { get; private set; }
    public SlimeTowerAttackState AttackState { get; private set; }

    public SlimeTowerGrabbedState GrabbedState { get; private set; }
    public SlimeTowerWalkState WalkState { get; private set; }

 
    public SlimeStateMachine(BaseSlimeTower slimeTower)
    {
        IdleState = new SlimeTowerIdleState(this);
        AttackState = new SlimeTowerAttackState(this);
        GrabbedState = new SlimeTowerGrabbedState(this);
        WalkState = new SlimeTowerWalkState(this);
        SlimeTower = slimeTower;
    }
}