using _Script.Infrastructure.Factories;
using _Script.Infrastructure.Factories.Fsm;
using _Script.Infrastructure.FSM;
using _Script.Infrastructure.FSM.States;
using VContainer;
using VContainer.Unity;

namespace _Script.Infrastructure.Bootstrap
{
    public class Bootstrapper : IInitializable
    {
        private IStateMachine _stateMachine;
        private IStateFactory _stateFactory;

        [Inject]
        private void Construct(IStateMachine stateMachine, IStateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
        }

        public void Initialize()
        {
            RegisterState();
            
            _stateMachine.Enter<BootstrapState>();
        }

        private void RegisterState()
        {
            _stateMachine.RegisterState<BootstrapState>(_stateFactory.CreateSystem<BootstrapState>());
            _stateMachine.RegisterState<SceneLoadState>(_stateFactory.CreateSystem<SceneLoadState>());
        }
    }
}