using _Script.Infrastructure.Data.StaticData;
using _Script.Infrastructure.Factories;
using _Script.Infrastructure.Generator;
using _Script.Infrastructure.ScenesLoader;
using Cysharp.Threading.Tasks;

namespace _Script.Infrastructure.FSM.States
{
    public class SceneLoadState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IGameGenerator _gameGenerator;
        private readonly IRopeFactory _ropeFactory;
        private readonly INodeFactory _nodeFactory;
        private readonly IWinWindowFactory _windowFactory;
        private readonly IScoresFactory _scoresFactory;
        private readonly ISkipButtonFactory _skipButtonFactory;

        public SceneLoadState(ISceneLoader sceneLoader,
            IStaticDataProvider staticDataProvider,
            IGameGenerator gameGenerator,
            IRopeFactory ropeFactory,
            INodeFactory nodeFactory,
            IWinWindowFactory windowFactory,
            IScoresFactory scoresFactory,
            ISkipButtonFactory skipButtonFactory)
        {
            _sceneLoader = sceneLoader;
            _staticDataProvider = staticDataProvider;
            _gameGenerator = gameGenerator;
            _ropeFactory = ropeFactory;
            _nodeFactory = nodeFactory;
            _windowFactory = windowFactory;
            _scoresFactory = scoresFactory;
            _skipButtonFactory = skipButtonFactory;
        }

        public async void Enter()
        {
            await _sceneLoader.LoadScene(_staticDataProvider
                .AssetsReferences.MainSceneReference);
            
            await InitializeFactories();
            GenerateGame();
        }

        private async UniTask InitializeFactories()
        {
            await _ropeFactory.Initialize();
            await _nodeFactory.Initialize();
            await _windowFactory.Initialize();
            await _scoresFactory.Initialize();
            await _skipButtonFactory.Initialize();
            
            _skipButtonFactory.CreateSkipButton();
            _windowFactory.CreateWinWindow();
            _scoresFactory.CreateScoreWindow();
        }

        private void GenerateGame()
        {
            _gameGenerator.Initialize();
            _gameGenerator.GenerateRandomNodesAndRopes();
        }

        public void Exit()
        {
        }
    }
}