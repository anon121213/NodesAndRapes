using _Script.Gameplay.WinSystem.WinUi;
using UnityEngine;

namespace _Script.Infrastructure.Factories.UI
{
    public class WinWindowFactory : IWinWindowFactory
    {
        private readonly IWinPresenter _winPresenter;
        private readonly Canvas _canvas;

        private GameObject _window;

        public WinWindowFactory(IWinPresenter winPresenter) => 
            _winPresenter = winPresenter;

        public void InitializeWinWindow(Transform window, IWinView winView)
        {
            window.transform.localScale = Vector3.zero;
            window.transform.position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            
            _winPresenter.Initialize(winView, window.transform);
        }
    }
}