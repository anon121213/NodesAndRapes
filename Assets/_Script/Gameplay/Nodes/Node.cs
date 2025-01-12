using System;
using System.Collections.Generic;
using _Script.Gameplay.Nodes.Mover;
using _Script.Gameplay.Nodes.NodeUi;
using _Script.Gameplay.Ropes;
using _Script.Gameplay.SoundSystem;
using _Script.Gameplay.SoundSystem.Data;
using _Script.Infrastructure.Data.StaticData;
using UnityEngine;

namespace _Script.Gameplay.Nodes
{
    public class Node : MonoBehaviour
    {
        private readonly List<Rope> Ropes = new();
        
        private INodeMover _nodeMover;
        private INodePresenter _nodePresenter;
        private ISoundService _soundService;
        private IStaticDataProvider _staticDataProvider;

        private float _soundCooldown; 
        private float _lastSoundTime;
        
        private int _maxRopeCount;
        private Vector2 _offset;

        public event Action<bool> OnMouseOver;

        public void Initialize(INodeMover nodeMover,
            INodePresenter nodePresenter,
            ISoundService soundService,
            IStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
            _nodeMover = nodeMover;
            _nodePresenter = nodePresenter;
            _soundService = soundService;

            _maxRopeCount = _staticDataProvider.NodesGeneratorConfig.MaxRopeCount;
            _soundCooldown = _staticDataProvider.NodesGeneratorConfig.SoundCooldown;
            
            _nodeMover.OnNodeDrag += UpdateRopes;
        }

        private void UpdateRopes()
        {
            foreach (Rope rope in Ropes) 
                rope.UpdateRope();
        }

        public void AddRope(Rope rope)
        {
            if (Ropes.Count > _maxRopeCount)
                return;
            
            Ropes.Add(rope);
        }

        private void OnMouseDown()
        {
            _nodeMover.OnMouseDown();
            _soundService.PlayOnceSound(SoundType.OnNodeMouseDown);
            _lastSoundTime = Time.time; 
        }

        private void OnMouseDrag()
        {
            _nodeMover.OnMouseDrag();

            if (!(Time.time - _lastSoundTime >= _soundCooldown))
                return;
            
            _soundService.PlayOnceSound(SoundType.OnNodeDrag);
            _lastSoundTime = Time.time;
        }

        private void OnMouseExit() => 
            OnMouseOver?.Invoke(false);

        private void OnMouseEnter() => 
            OnMouseOver?.Invoke(true);

        public void Release() => 
            Ropes.Clear();

        private void OnDestroy()
        {
            _nodeMover.OnNodeDrag -= UpdateRopes;
            _nodePresenter.Dispose();
        }
    }
}
