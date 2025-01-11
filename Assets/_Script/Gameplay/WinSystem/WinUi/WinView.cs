using UnityEngine;
using UnityEngine.UI;

namespace _Script.Gameplay.WinSystem.WinUi
{
    public class WinView : MonoBehaviour, IWinVie
    {
        [field: SerializeField] public Button RestartButton { get; private set; }
    }

    public interface IWinVie
    { 
        public Button RestartButton { get; }
    }
}