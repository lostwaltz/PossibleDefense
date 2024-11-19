using UnityEngine;

public class SlimeTowerIdleState : SlimeTowerBaseState
{
    private  int _targetLayerMask = LayerMask.GetMask("Enemy");
    private Collider[] _results = new Collider[10];  //상수 ? 사용 



    public SlimeTowerIdleState(SlimeStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.SlimeTower.AnimatorHashData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.SlimeTower.AnimatorHashData.IdleParameterHash);
    }

    public override void Update()
    {
        int count = Physics.OverlapSphereNonAlloc(stateMachine.SlimeTower.transform.position, _attackRange, _results);

        if (count <= 0) return;

        // 충돌한 경우 처리
        for (int i = 0; i < count; i++)
        {
            var collider = _results[i];
            if ((_targetLayerMask & (1 << collider.gameObject.layer)) == 0) continue;

            stateMachine.SlimeTower.Target = collider.transform;
            stateMachine.ChangeState(stateMachine.AttackState);
            break; 
        }
    }

    
    public override void FixedUpdate()
    {
    }

}




