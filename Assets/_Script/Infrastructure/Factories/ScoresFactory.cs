using _Script.Gameplay.ScoreSystem;
using _Script.Infrastructure.Data.AddressableLoader;
using _Script.Infrastructure.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Infrastructure.Factories
{
    public class ScoresFactory : IScoresFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly IScorePresenter _scoresPresenter;
        private readonly Canvas _canvas;

        private GameObject _scoreWindow;
        
        public ScoresFactory(IStaticDataProvider staticDataProvider,
            IAssetProvider assetProvider,
            IScorePresenter scoresPresenter,
            Canvas canvas)
        {
            _staticDataProvider = staticDataProvider;
            _assetProvider = assetProvider;
            _scoresPresenter = scoresPresenter;
            _canvas = canvas;
        }

        public async UniTask Initialize() =>
            _scoreWindow = await _assetProvider.LoadAsync<GameObject>
                (_staticDataProvider.AssetsReferences.ScoreReference);

        public void CreateScoreWindow()
        {
            GameObject window = Object.Instantiate(_scoreWindow, _canvas.transform);

            window.transform.position = new Vector3(300, 300);
            
            IScoresView scoresView = window.GetComponent<ScoresView>();
            
            _scoresPresenter.Initialize(scoresView);
        }
    }

    public interface IScoresFactory
    {
        UniTask Initialize();
        void CreateScoreWindow();
    }
}