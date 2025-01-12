using _Script.Gameplay.ScoreSystem;
using _Script.Gameplay.SkipButton;
using _Script.Gameplay.WinSystem.WinUi;
using _Script.Infrastructure.Data.AddressableLoader;
using _Script.Infrastructure.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Infrastructure.Factories.UI
{
    public class InterfaceFactory : IInterfaceFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IWinWindowFactory _windowFactory;
        private readonly IScoresFactory _scoresFactory;
        private readonly ISkipButtonFactory _skipButtonFactory;

        private GameObject _mainInterface;

        public InterfaceFactory(IStaticDataProvider staticDataProvider,
            IAddressablesLoader addressablesLoader,
            IWinWindowFactory windowFactory,
            IScoresFactory scoresFactory,
            ISkipButtonFactory skipButtonFactory)
        {
            _staticDataProvider = staticDataProvider;
            _addressablesLoader = addressablesLoader;
            _windowFactory = windowFactory;
            _scoresFactory = scoresFactory;
            _skipButtonFactory = skipButtonFactory;
        }

        public async UniTask Initialize()
        {
            GameObject mainInterface = await _addressablesLoader.LoadAsync<GameObject>
                (_staticDataProvider.AssetsReferences.MainInterfaceReference);

            _mainInterface = Object.Instantiate(mainInterface);
            
            WinView winView = _mainInterface.GetComponentInChildren<WinView>();
            IScoresView scoresView = _mainInterface.GetComponentInChildren<IScoresView>();
            ISkipView skipView = _mainInterface.GetComponentInChildren<ISkipView>();
            
            _windowFactory.InitializeWinWindow(winView.transform, winView);
            _scoresFactory.InitializeScoreWindow(scoresView);
            _skipButtonFactory.InitializeSkipButton(skipView);
        }
    }
}