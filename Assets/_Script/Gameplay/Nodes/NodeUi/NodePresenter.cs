namespace _Script.Gameplay.Nodes.NodeUi
{
    public class NodePresenter : INodePresenter
    {
        private Node _node;
        private INodeView _nodeView;

        public void Initialize(Node node,
            INodeView nodeView)
        {
            _node = node;
            _nodeView = nodeView;
            _node.OnMouseOver += SwitchSprite;
        }

        private void SwitchSprite(bool value) => 
            _nodeView.OverSpriteSwitcher(value);


        public void Dispose() => 
            _node.OnMouseOver -= SwitchSprite;
    }

    public interface INodePresenter
    {
        void Initialize(Node node,
            INodeView nodeView);
        void Dispose();
    }
}