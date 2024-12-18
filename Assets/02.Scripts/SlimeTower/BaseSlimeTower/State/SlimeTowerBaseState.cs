//모든 상태에서 공통 적인 부분을 담당하는 클래스

public class SlimeTowerBaseState : IState
{
    protected SlimeStateMachine stateMachine;

    
    public SlimeTowerBaseState(SlimeStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
     }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }
    
    protected void StartAnimation(int animatorHash)
    {
        stateMachine.SlimeTower.Animator.SetBool(animatorHash, true);
    }
    
    protected void StartAnimationTrigger(int animatorHash)
    {
        stateMachine.SlimeTower.Animator.SetTrigger(animatorHash);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.SlimeTower.Animator.SetBool(animatorHash, false);
    }
  

}                                         