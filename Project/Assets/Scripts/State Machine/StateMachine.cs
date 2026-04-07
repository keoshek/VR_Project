namespace Ata.StateMachine
{
    [System.Serializable]
    public class StateMachine
    {
        public IState CurrentState { get; private set; }
        public string CurrentStateName;

        // add states here

        public void Initialize(IState startingState)
        {
            CurrentState = startingState;
            CurrentStateName = CurrentState.ToString();
            startingState.Enter();
        }

        public void TransitionTo(IState nextState)
        {
            if (CurrentState == nextState) return;

            CurrentState.Exit();
            CurrentState = nextState;
            CurrentStateName = CurrentState.ToString();
            nextState.Enter();
        }

        public void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void LateUpdate()
        {
            CurrentState?.LateUpdate();
        }

        public bool IsCurrentState(IState state)
        {
            return CurrentState == state;
        }
    }
}