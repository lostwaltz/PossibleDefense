
    public class BaseStateMachine
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
