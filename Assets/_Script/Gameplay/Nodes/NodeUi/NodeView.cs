using UnityEngine;

namespace _Script.Gameplay.Nodes.NodeUi
{
    public class NodeView : MonoBehaviour, INodeView
    {
        [SerializeField] private GameObject _overSprite;

        public void OverSpriteSwitcher(bool value) => 
            _overSprite.SetActive(value);
    }

    public interface INodeView
    {
        void OverSpriteSwitcher(bool value);
    }
}