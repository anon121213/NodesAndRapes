using _Script.Gameplay.WinSystem.WinUi;
using _Script.Infrastructure.Data.AddressableLoader;
using _Script.Infrastructure.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Infrastructure.Factories
{
    public class WinWindowFactory : IWinWindowFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly IWinPresenter _winPresenter;
        private readonly Canvas _canvas;

        private GameObject _window;

        public WinWindowFactory(IStaticDataProvider staticDataProvider,
            IAssetProvider assetProvider,
            IWinPresenter winPresenter,
            Canvas canvas)
        {
            _staticDataProvider = staticDataProvider;
            _assetProvider = assetProvider;
            _winPresenter = winPresenter;
            _canvas = canvas;
        }

        public async UniTask Initialize() =>
            _window = await _assetProvider.LoadAsync<GameObject>(
                _staticDataProvider.AssetsReferences.WinWindowReference);

        public void CreateWinWindow()
        {
            GameObject window = Object.Instantiate(_window, _canvas.transform);
            
            window.transform.localScale = Vector3.zero;
            window.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            
            IWinVie view = window.GetComponent<WinView>();
            
            _winPresenter.Initialize(view, window.transform);
        }
    }

    public interface IWinWindowFactory
    {
        UniTask Initialize();
        void CreateWinWindow();
    }
}