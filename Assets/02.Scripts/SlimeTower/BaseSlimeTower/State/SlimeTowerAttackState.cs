using UnityEngine;

public class SlimeTowerAttackState : SlimeTowerBaseState
{
    private float _attackSpeed;
    private float _attackCoolTime = 0f;
    private float _lastAttackTime = 0f;

    public SlimeTowerAttackState(SlimeStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAttackSpeed();
        stateMachine.SlimeTower.StatHandler.OnIncreaseStatEvent += SetAttackSpeed;
    }


    public override void Exit()
    {
        base.Exit();
        stateMachine.SlimeTower.StatHandler.OnIncreaseStatEvent -= SetAttackSpeed;
        StopAnimation(stateMachine.SlimeTower.AnimatorHashData.AttackParameterHash);
    }

    public override void Update()
    {
        if (Time.time >= _attackCoolTime + _lastAttackTime)
        {
            CheckAndAttackTarget();
        }

        if (!stateMachine.SlimeTower.Target.gameObject.activeSelf)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    private void CheckAndAttackTarget()
    {
        bool isTargetInRange = Physics.CheckSphere(stateMachine.SlimeTower.transform.position, _attackRange,
            1 << stateMachine.SlimeTower.Target.gameObject.layer);

        if (!isTargetInRange)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }


        stateMachine.SlimeTower.transform.LookAt(stateMachine.SlimeTower.Target);
        Attack();
    }


    private void SetAttackSpeed()
    {
        _attackSpeed = stateMachine.SlimeTower.StatHandler.AttackSpeed;
        _attackCoolTime = 1f / _attackSpeed;

        if (_attackSpeed > 1)
            stateMachine.SlimeTower.Animator.SetFloat(stateMachine.SlimeTower.AnimatorHashData.AttackSpeedParameterHash,
                _attackSpeed);
        else
            stateMachine.SlimeTower.Animator.SetFloat(stateMachine.SlimeTower.AnimatorHashData.AttackSpeedParameterHash,
                1);
    }


    private void Attack()
    {
        _lastAttackTime = Time.time;
        StartAnimationTrigger(stateMachine.SlimeTower.AnimatorHashData.AttackParameterHash);
        stateMachine.SlimeTower.AttackStrategy.Execute(stateMachine.SlimeTower.Target);
    }
}