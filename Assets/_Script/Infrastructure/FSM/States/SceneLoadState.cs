using _Script.Infrastructure.Data.StaticData;
using _Script.Infrastructure.Factories;
using _Script.Infrastructure.Generator;
using _Script.Infrastructure.ScenesLoader;

namespace _Script.Infrastructure.FSM.States
{
    public class SceneLoadState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IGameGenerator _gameGenerator;
        private readonly IRopeFactory _ropeFactory;
        private readonly INodeFactory _nodeFactory;

        public SceneLoadState(ISceneLoader sceneLoader,
            IStaticDataProvider staticDataProvider,
            IGameGenerator gameGenerator,
            IRopeFactory ropeFactory,
            INodeFactory nodeFactory)
        {
            _sceneLoader = sceneLoader;
            _staticDataProvider = staticDataProvider;
            _gameGenerator = gameGenerator;
            _ropeFactory = ropeFactory;
            _nodeFactory = nodeFactory;
        }

        public async void Enter()
        {
            await _sceneLoader.LoadScene(_staticDataProvider
                .AssetsReferences.MainSceneReference);
            
            GenerateGame();
        }

        private async void GenerateGame()
        {
            await _ropeFactory.Initialize();
            await _nodeFactory.Initialize();
            
            _gameGenerator.Initialize();
            _gameGenerator.GenerateRandomNodesAndRopes();
        }

        public void Exit()
        {
        }
    }
}