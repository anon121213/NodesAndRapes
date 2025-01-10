using _Script.Infrastructure.Data.StaticData;
using _Script.Infrastructure.ScenesLoader;

namespace _Script.Infrastructure.FSM.States
{
    public class SceneLoadState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataProvider _staticDataProvider;

        public SceneLoadState(ISceneLoader sceneLoader,
            IStaticDataProvider staticDataProvider)
        {
            _sceneLoader = sceneLoader;
            _staticDataProvider = staticDataProvider;
        }

        public async void Enter() => 
            await _sceneLoader.LoadScene(_staticDataProvider.AssetsReferences.MainSceneReference);

        public void Exit()
        {
        }
    }
}