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
    }

    private void CheckAndAttackTarget()
    {
        if (stateMachine.SlimeTower.Target == null || !stateMachine.SlimeTower.Target.gameObject.activeSelf)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        var distanceToTarget = Vector3.Distance(stateMachine.SlimeTower.transform.position,
            stateMachine.SlimeTower.Target.position);

        Collider targetCollider = stateMachine.SlimeTower.Target.GetComponent<Collider>();
        var adjustedAttackRange = stateMachine.SlimeTower.StatHandler.AttackRange;

        if (targetCollider != null)
        {
            adjustedAttackRange += targetCollider.bounds.extents.magnitude;
        }

        if (distanceToTarget > adjustedAttackRange)
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