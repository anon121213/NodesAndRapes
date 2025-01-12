using System.Collections.Generic;
using _Script.Gameplay.Nodes;
using _Script.Gameplay.Pools;
using _Script.Gameplay.Ropes;
using _Script.Gameplay.Ropes.Checker;
using _Script.Gameplay.WinSystem.Checker;
using _Script.Infrastructure.Data.AddressableLoader;
using _Script.Infrastructure.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Infrastructure.Factories.Services
{
    public class RopeFactory : IRopeFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IWinService _winService;

        private RopePool _ropePool;
        private GameObject _ropePrefab;
        
        private Material _redMat;
        private Material _greenMat;

        private List<Rope> _ropes = new();

        public RopeFactory(IAddressablesLoader addressablesLoader,
            IStaticDataProvider staticDataProvider,
            IWinService winService)
        {
            _addressablesLoader = addressablesLoader;
            _staticDataProvider = staticDataProvider;
            _winService = winService;
        }

        public async UniTask Initialize()
        {
            _ropePrefab = await _addressablesLoader.LoadAsync<GameObject>
                (_staticDataProvider.AssetsReferences.RopeReference);
            
            _greenMat = await _addressablesLoader.LoadAsync<Material>
                (_staticDataProvider.RopesConfig.GreenMaterial);
            
            _redMat = await _addressablesLoader.LoadAsync<Material>
                (_staticDataProvider.RopesConfig.RegMaterial);
            
            _ropePool = new RopePool(_ropePrefab.GetComponent<Rope>(), null, 
                _staticDataProvider.NodesGeneratorConfig.NodeCount * 2);
        }
        
        public Rope GetRope(Node firstNode, Node secondNode)
        {
            Rope rope = _ropePool.GetObject();
            
            InitRopeChecker(rope);
            
            _winService.AddRope(rope);
            
            rope.Initialize(firstNode, secondNode);
            
            rope.UpdateRope();
            
            return rope;
        }

        private void InitRopeChecker(Rope rope)
        {
            if (_ropes.Contains(rope))
                return;
            
            _ropes.Add(rope);
                
            IIntersectionChecker intersectionChecker =
                new IntersectionChecker(rope, _greenMat, _redMat, _staticDataProvider);
                
            rope.SetChecker(intersectionChecker);
        }
        
        public void ReturnToPool(Rope rope)
        {
            _winService.RemoveRope(rope);
            _ropePool.ReturnObject(rope);
        }
    }
}