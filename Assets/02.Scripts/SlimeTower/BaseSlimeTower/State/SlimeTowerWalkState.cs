using UnityEngine;

public class SlimeTowerWalkState : SlimeTowerBaseState
{
    public Transform target;
    private float moveSpeed = 5f;

    public SlimeTowerWalkState(SlimeStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("워크스테이트");
        StartAnimation(stateMachine.SlimeTower.AnimatorHashData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.SlimeTower.AnimatorHashData.WalkParameterHash);
    }

    
    public override void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, stateMachine.SlimeTower.transform.position.y,
            target.position.z);

        stateMachine.SlimeTower.transform.LookAt(target);

        stateMachine.SlimeTower.transform.position = Vector3.MoveTowards(
            stateMachine.SlimeTower.transform.position, 
            targetPosition,
            moveSpeed * Time.deltaTime 
        );

        if (Vector3.Distance(stateMachine.SlimeTower.transform.position, targetPosition) <= 0.01f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

}