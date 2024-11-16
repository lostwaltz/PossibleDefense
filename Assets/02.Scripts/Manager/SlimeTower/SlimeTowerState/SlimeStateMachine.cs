
//Enemy에서도 사용할 경우 하나로 합치기 ! 
public class SlimeStateMachine
{
    protected IState currentState;
    
    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
    
    public void Update()
    {
        currentState?.Update();
    }
    
    public void FixedUpdateState()
    {
        currentState?.Update();
    }
    
    
}