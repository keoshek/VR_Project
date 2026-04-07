namespace Ata.StateMachine
{
    [System.Serializable]
    public class TabletStateMachine
    {
        public IState CurrentState { get; private set; }
        public string CurrentStateName;

        // add states here
        public TabletHomeState HomeState => _homeState ??= new(source);
        public TabletHomeState _homeState;
        public TabletSettingsState SettingsState => _settingsState ??= new(source);
        public TabletSettingsState _settingsState;
        public TabletRoomsState RoomsState => _roomsState ??= new(source);
        public TabletRoomsState _roomsState;

        private TabletUI source;

        public TabletStateMachine(TabletUI _source) 
        {
            source = _source;
        }

        public void Initialize(IState startingState)
        {
            HomeState.Exit();
            SettingsState.Exit();
            RoomsState.Exit();

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