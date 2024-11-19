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
        StartAnimation(stateMachine.SlimeTower.animatorHashData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.SlimeTower.animatorHashData.WalkParameterHash);
    }

    
    //강화, 스폰 , 판매, 카메라 
    
    // 슬라임이 길게 눌리면 시네머신 통해 하늘에서 바라보는 시점 (0.3 초 이동 모드) , 
    // 타일이 눌려서 슬라임이 이동 시작하면 시점 원상태 
    
    //슬라임을 빠르게 클리하면 -> 시네머신 
    // 유저 탓이다. , 왜 빠르게 눌렀냐 ? 
    // 보간 이동 없이 바로 화면 전환 해봅시다. up VCam  origin VCam
    
    
    
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