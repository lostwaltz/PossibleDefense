using UnityEngine;

public class SlimeTowerIdleState : SlimeTowerBaseState
{
    private int _targetLayer = LayerMask.GetMask("Enemy");
    private RaycastHit[] _hits = new RaycastHit[10];
    
    
    float radius = 20f; // 나중에 공격 범위를 가져와서 하도록 처리! 

    public SlimeTowerIdleState(SlimeStateMachine _stateMachine) : base(_stateMachine)
    {
        
        
        
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.SlimeTower.animatorHashData.IdleParameterHash);
     }

    public override void Exit()
    {
        StopAnimation(stateMachine.SlimeTower.animatorHashData.IdleParameterHash);
    }

    public override void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(stateMachine.SlimeTower.transform.position, radius);

        if (colliders.Length > 0)
        {
            // 충돌한 경우 처리
            foreach (var collider in colliders)
            {
                stateMachine.SlimeTower.Target = collider.transform;
                stateMachine.ChangeState(stateMachine.AttackState);
            }
        }

    }

    public override void FixedUpdate()
    {
        
    }
    
    
}