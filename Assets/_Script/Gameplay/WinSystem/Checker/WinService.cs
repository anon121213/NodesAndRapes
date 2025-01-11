using System;
using System.Collections.Generic;
using System.Linq;
using _Script.Gameplay.Ropes;
using UnityEngine;

namespace _Script.Gameplay.WinSystem.Checker
{
    public class WinService : IWinService, IDisposable
    {
        private readonly Dictionary<Rope, bool> _ropes = new();

        public bool IsWin { get; private set; }
        public event Action OnWin;

        public void AddRope(Rope rope)
        {
            _ropes.TryAdd(rope, false);
            rope.IntersectionChecker.ChangeIntersection += ChangeWinGrade;
        }

        public void RemoveRope(Rope rope)
        {
            if (_ropes.ContainsKey(rope)) 
                _ropes.Remove(rope);
        }

        public void Restart() => 
            IsWin = false;

        private void ChangeWinGrade(Rope rope, bool check)
        {
            _ropes[rope] = check;

            if (!CheckWin()) 
                return;
            
            Debug.Log("win");
            IsWin = true;
            OnWin?.Invoke();
        }

        private bool CheckWin() => 
            _ropes.Values.All(value => value) && !IsWin;

        public void Dispose()
        {
            foreach (var rope in _ropes) 
                rope.Key.IntersectionChecker.ChangeIntersection -= ChangeWinGrade;
        }
    }

    public interface IWinService : IWineble
    {
        void AddRope(Rope rope);
        void RemoveRope(Rope rope);
      
    }

    public interface IWineble
    {
        void Restart();
        bool IsWin { get; }
        event Action OnWin;
    }
}