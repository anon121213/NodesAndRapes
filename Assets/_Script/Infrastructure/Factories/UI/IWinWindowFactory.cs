using _Script.Gameplay.WinSystem.WinUi;
using UnityEngine;

namespace _Script.Infrastructure.Factories.UI
{
    public interface IWinWindowFactory
    {
        void InitializeWinWindow(Transform window, IWinView winView);
    }
}