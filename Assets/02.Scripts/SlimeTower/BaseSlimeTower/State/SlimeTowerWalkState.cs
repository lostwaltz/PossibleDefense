using UnityEngine;

public class SlimeTowerWalkState : SlimeTowerBaseState
{
    public Transform target; // 타워 컨트롤러에서 설정 해줌! 
    private float moveSpeed = 5f;
    private float threshold = 0.1f;

    public SlimeTowerWalkState(SlimeStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.SlimeTower.AnimatorHashData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.SlimeTower.AnimatorHashData.WalkParameterHash);
    }


    public override void Update()
    {
        Vector3 currentPosition = stateMachine.SlimeTower.transform.position;
        Vector3 targetPosition = new Vector3(target.position.x, currentPosition.y, target.position.z);

        stateMachine.SlimeTower.transform.LookAt(targetPosition);

        stateMachine.SlimeTower.transform.position = Vector3.MoveTowards(
            currentPosition,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(currentPosition, targetPosition) <= threshold)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}