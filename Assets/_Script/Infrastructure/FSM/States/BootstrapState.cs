namespace _Script.Infrastructure.FSM.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;

        public BootstrapState(IStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Enter() =>
            _stateMachine.Enter<SceneLoadState>();

        public void Exit()
        {
        }
    }
}