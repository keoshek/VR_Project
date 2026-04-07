namespace Ata.StateMachine
{
    public interface IState
    {
        public void Enter()
        {
            // code that runs when we first enter the status
        }

        public void FixedUpdate()
        {
            // fixed per-frame logic
        }

        public void Update()
        {
            // per-frame logic, include condition to transition to a new status
        }

        public void LateUpdate()
        {
            // late per-frame logic
        }

        public void Exit()
        {
            // code that runs when we exit the status
        }

        public void Action1()
        {
            // any custom function
        }
    }
}