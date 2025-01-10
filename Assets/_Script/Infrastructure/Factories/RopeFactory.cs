using System.Collections.Generic;
using _Script.Gameplay.Pools;
using _Script.Gameplay.Ropes;
using _Script.Infrastructure.Data.AddressableLoader;
using _Script.Infrastructure.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Infrastructure.Factories
{
    public class RopeFactory : IRopeFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataProvider _staticDataProvider;
        
        private RopePool _ropePool;

        private GameObject _ropePrefab;

        public RopeFactory(IAssetProvider assetProvider,
            IStaticDataProvider staticDataProvider)
        {
            _assetProvider = assetProvider;
            _staticDataProvider = staticDataProvider;
        }

        public async UniTask Initialize()
        {
            _ropePrefab = await _assetProvider.LoadAsync<GameObject>
                (_staticDataProvider.AssetsReferences.RopeReference);
            
            _ropePool = new RopePool(_ropePrefab.GetComponent<Rope>(), null, 
                _staticDataProvider.NodesGeneratorConfig.NodeCount * 2);
        }
        
        public Rope GetRope(Transform firstNode, Transform secondNode)
        {
            Rope rope = _ropePool.GetObject();
            rope.Initialize(firstNode.transform, secondNode.transform);
            
            rope.UpdateRope();
            
            return rope;
        }

        public void ReturnToPool(Rope rope) => 
            _ropePool.ReturnObject(rope);
    }
}