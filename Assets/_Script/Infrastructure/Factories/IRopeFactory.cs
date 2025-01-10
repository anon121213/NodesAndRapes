using _Script.Gameplay.Ropes;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Infrastructure.Factories
{
    public interface IRopeFactory
    {
        UniTask Initialize();
        Rope GetRope(Transform firstNode, Transform secondNode);
        void ReturnToPool(Rope rope);
    }
}