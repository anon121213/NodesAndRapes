using UnityEngine;
using UnityEngine.UI;

namespace _Script.Gameplay.WinSystem.WinUi
{
    public class WinView : MonoBehaviour, IWinView
    {
        [field: SerializeField] public Button RestartButton { get; private set; }
    }

    public interface IWinView
    { 
        public Button RestartButton { get; }
    }
}