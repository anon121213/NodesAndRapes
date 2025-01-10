using _Script.Gameplay.Nodes;
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
        
        private Material _redMat;
        private Material _greenMat;

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
            
            _greenMat = await _assetProvider.LoadAsync<Material>
                (_staticDataProvider.RopesConfig.GreenMaterial);
            
            _redMat = await _assetProvider.LoadAsync<Material>
                (_staticDataProvider.RopesConfig.RegMaterial);
            
            _ropePool = new RopePool(_ropePrefab.GetComponent<Rope>(), null, 
                _staticDataProvider.NodesGeneratorConfig.NodeCount * 2);
        }
        
        public Rope GetRope(Node firstNode, Node secondNode)
        {
            Rope rope = _ropePool.GetObject();
            
            rope.Initialize(firstNode, secondNode, _greenMat, _redMat);
            
            rope.UpdateRope();
            
            return rope;
        }

        public void ReturnToPool(Rope rope) => 
            _ropePool.ReturnObject(rope);
    }
}