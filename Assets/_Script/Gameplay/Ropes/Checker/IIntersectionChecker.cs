using System;
using UnityEngine;

namespace _Script.Gameplay.Ropes.Checker
{
    public interface IIntersectionChecker : IIntersectionWinner
    {
        void Initialize();
        void InitInterception();
        void UpdateBoxCollider(Vector3 startPos, Vector3 endPos);
        void OnTriggerEnter2D(Collider2D collision);
        void OnTriggerStay2D(Collider2D collision);
        void OnTriggerExit2D(Collider2D collision);
    }

    public interface IIntersectionWinner
    {
        event Action<Rope, bool> ChangeIntersection;
    }
}