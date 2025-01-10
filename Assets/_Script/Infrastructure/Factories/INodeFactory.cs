using _Script.Gameplay.Nodes;
using Cysharp.Threading.Tasks;

namespace _Script.Infrastructure.Factories
{
    public interface INodeFactory
    {
        UniTask Initialize();
        Node GetNode(float maxX, float maxY, float minX, float minY);
        void ReturnToPool(Node rope);
    }
}