using System.Collections.Generic;
using _Script.Gameplay.Ropes;
using UnityEngine;

namespace _Script.Gameplay
{
    public class IntersectionChecker : IIntersectionChecker
    {
        public void CheckForRopeIntersections(List<Rope> allRopes)
        {
            foreach (var rope1 in allRopes)
            {
                foreach (var rope2 in allRopes)
                {
                    if (rope1 == rope2)
                        continue;

                    if (AreSegmentsIntersecting(rope1.StartNode.transform.position,
                            rope1.EndNode.transform.position,
                            rope2.StartNode.transform.position,
                            rope2.EndNode.transform.position))
                    {
                        rope1.SetIntersection(false); 
                        rope2.SetIntersection(false); 
                        continue;
                    }

                    rope1.SetIntersection(true); 
                    rope2.SetIntersection(true); 
                }
            }
        }

        private bool AreSegmentsIntersecting(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
        {
            float d1 = Direction(q1, q2, p1);
            float d2 = Direction(q1, q2, p2);
            float d3 = Direction(p1, p2, q1);
            float d4 = Direction(p1, p2, q2);

            return (d1 > 0 && d2 < 0 || d1 < 0 && d2 > 0) && (d3 > 0 && d4 < 0 || d3 < 0 && d4 > 0);
        }

        private float Direction(Vector2 pi, Vector2 pj, Vector2 pk) => 
            (pk.x - pi.x) * (pj.y - pi.y) - (pj.x - pi.x) * (pk.y - pi.y);
    }

    public interface IIntersectionChecker
    {
        void CheckForRopeIntersections(List<Rope> allRopes);
    }
}