using _Script.Gameplay.Nodes;
using _Script.Gameplay.Ropes;
using Cysharp.Threading.Tasks;

namespace _Script.Infrastructure.Factories.Services
{
    public interface IRopeFactory
    {
        UniTask Initialize();
        Rope GetRope(Node firstNode, Node secondNode);
        void ReturnToPool(Rope rope);
    }
}