using _Script.Infrastructure.Factories;
using _Script.Infrastructure.Generator;

namespace _Script.Infrastructure.FSM.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IGameGenerator _gameGenerator;
        private readonly IRopeFactory _ropeFactory;
        private readonly INodeFactory _nodeFactory;

        public BootstrapState(IStateMachine stateMachine,
            IGameGenerator GameGenerator,
            IRopeFactory ropeFactory,
            INodeFactory nodeFactory)
        {
            _stateMachine = stateMachine;
            _gameGenerator = GameGenerator;
            _ropeFactory = ropeFactory;
            _nodeFactory = nodeFactory;
        }

        public async void Enter() =>
            _stateMachine.Enter<SceneLoadState>();

        public void Exit()
        {
        }
    }
}