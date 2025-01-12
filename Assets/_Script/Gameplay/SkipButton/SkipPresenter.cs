using System;
using _Script.Gameplay.WinSystem.Checker;
using _Script.Infrastructure.Generator;

namespace _Script.Gameplay.SkipButton
{
    public class SkipPresenter : ISkipPresenter, IDisposable
    {
        private readonly IGameGenerator _gameGenerator;
        private readonly IWinService _winService;

        private ISkipView _skipView;

        public SkipPresenter(IGameGenerator gameGenerator,
            IWinService winService)
        {
            _gameGenerator = gameGenerator;
            _winService = winService;
        }

        public void Initialize(ISkipView skipView)
        {
            _skipView = skipView;
            _skipView.SkipButton.onClick.AddListener(GenerateGame);
        }

        private void GenerateGame()
        {
            _winService.SkipLevel();
            _gameGenerator.GenerateRandomNodesAndRopes();
            _winService.Restart();
        }

        public void Dispose() => 
            _skipView.SkipButton.onClick.RemoveListener(GenerateGame);
    }

    public interface ISkipPresenter
    {
        void Initialize(ISkipView skipView);
    }
}