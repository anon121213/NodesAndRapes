using _Script.Gameplay.SkipButton;
using _Script.Infrastructure.Data.AddressableLoader;
using _Script.Infrastructure.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Infrastructure.Factories
{
    public class SkipButtonFactory : ISkipButtonFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly ISkipPresenter _skipPresenter;
        private readonly Canvas _canvas;

        private GameObject _skipButton;
        
        public SkipButtonFactory(IAddressablesLoader addressablesLoader,
            IStaticDataProvider staticDataProvider,
            ISkipPresenter skipPresenter,
            Canvas canvas)
        {
            _addressablesLoader = addressablesLoader;
            _staticDataProvider = staticDataProvider;
            _skipPresenter = skipPresenter;
            _canvas = canvas;
        }

        public async UniTask Initialize() =>
            _skipButton = await _addressablesLoader.LoadAsync<GameObject>
                (_staticDataProvider.AssetsReferences.SkipButtonReference);

        public void CreateSkipButton()
        {
            GameObject skipButton = Object.Instantiate(_skipButton, _canvas.transform);

            skipButton.transform.position = new Vector3(600, 300);
            
            ISkipView skipView = skipButton.GetComponent<SkipView>();
            
            _skipPresenter.Initialize(skipView);
        }
    }

    public interface ISkipButtonFactory
    {
        UniTask Initialize();
        void CreateSkipButton();
    }
}