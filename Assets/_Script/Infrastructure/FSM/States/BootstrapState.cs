using _Script.Gameplay.SoundSystem;
using _Script.Infrastructure.Data.StaticData;

namespace _Script.Infrastructure.FSM.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly ISoundService _soundService;

        public BootstrapState(IStateMachine stateMachine,
            IStaticDataProvider staticDataProvider,
            ISoundService soundService)
        {
            _stateMachine = stateMachine;
            _staticDataProvider = staticDataProvider;
            _soundService = soundService;
        }

        public void Enter()
        {
            _soundService.AddSounds(_staticDataProvider.SoundConfig.Sounds);
            _stateMachine.Enter<SceneLoadState>();
        }

        public void Exit()
        {
        }
    }
}