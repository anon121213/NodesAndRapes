using UnityEngine;
using UnityEngine.UI;

namespace _Script.Gameplay.SkipButton
{
    public class SkipView : MonoBehaviour, ISkipView
    {
        [field: SerializeField] public Button SkipButton { get; private set; }
    }

    public interface ISkipView
    {
        Button SkipButton { get; }
    }
}