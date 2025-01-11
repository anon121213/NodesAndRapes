using System;
using _Script.Gameplay.WinSystem.Checker;
using _Script.Infrastructure.Generator;
using DG.Tweening; 
using UnityEngine;

namespace _Script.Gameplay.WinSystem.WinUi
{
    public class WinPresenter : IWinPresenter, IDisposable
    {
        private readonly IWineble _winService;
        private readonly IGameGenerator _gameGenerator;
        
        private IWinVie _winVie;
        private Transform _windowTransform;

        public WinPresenter(IWinService winService,
            IGameGenerator gameGenerator)
        {
            _winService = winService;
            _gameGenerator = gameGenerator;
        }

        public void Initialize(IWinVie winVie, 
            Transform windowTransform)
        {
            _windowTransform = windowTransform;
            _winVie = winVie;
            _winService.OnWin += Win;
            _winVie.RestartButton.onClick.AddListener(RestartLevel);
        }

        private void RestartLevel()
        {
            PlayWinWindowCloseAnim();
            _winService.Restart();
            _gameGenerator.GenerateRandomNodesAndRopes();
        }
        
        private void Win() => 
            PlayWinWindowOpenAnim();

        private void PlayWinWindowOpenAnim()
        {
            _windowTransform.localScale = Vector3.zero; 
            _windowTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce); 
        }

        private void PlayWinWindowCloseAnim() => 
            _windowTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);

        public void Dispose()
        {
            _winService.OnWin -= Win;
            _winVie.RestartButton.onClick.RemoveListener(RestartLevel);
        }
    }

    public interface IWinPresenter
    {
        void Initialize(IWinVie winVie, 
            Transform windowTransform);
    }
}
