using UnityEngine;

public class SlimeTowerAttackState : SlimeTowerBaseState
{
    private float _attackSpeed;
    private float _attackCoolTime = 0f;
    private float _lastAttackTime  = 0f;

    // TODO Stat 관련 접근은 추후 StatHandler를 통해서 할 수 있도록 변경
    public SlimeTowerAttackState(SlimeStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAttackSpeed();
        
    }

    
    public override void Exit()
    {
        StopAnimation(stateMachine.SlimeTower.animatorHashData.AttackParameterHash);
    }

    public override void Update()
    {
        if (  Time.time >= _attackCoolTime + _lastAttackTime)
        {
            Attack();
        }

        if (!stateMachine.SlimeTower.Target.gameObject.activeSelf)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }


    
    private void SetAttackSpeed()
    {
        _attackSpeed = stateMachine.SlimeTower.slimeTowerData.SlimeTowerStats.AttackSpeed;
        _attackCoolTime = 1f / _attackSpeed;
            
        if (_attackSpeed > 1)
            stateMachine.SlimeTower.animator.SetFloat(stateMachine.SlimeTower.animatorHashData.AttackSpeedParameterHash,
                _attackSpeed);
        else
            stateMachine.SlimeTower.animator.SetFloat(stateMachine.SlimeTower.animatorHashData.AttackSpeedParameterHash,
                1);
    }
    
    
    private void Attack()
    {
        _lastAttackTime = Time.time;
        StartAnimationTrigger(stateMachine.SlimeTower.animatorHashData.AttackParameterHash);
        stateMachine.SlimeTower.AttackStrategy.Execute(stateMachine.SlimeTower.Target);
    }

 
}