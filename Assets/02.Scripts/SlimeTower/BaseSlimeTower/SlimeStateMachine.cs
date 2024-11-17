
public class SlimeStateMachine : BaseStateMachine
{
    
    public BaseSlimeTower slimeTower { get;}
 
    public SlimeTowerIdleState IdleState { get; private set; }
 
    public SlimeStateMachine(BaseSlimeTower slimeTower)
    {
        IdleState = new SlimeTowerIdleState(this);
     }

}