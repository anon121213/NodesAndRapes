using UnityEngine;

namespace _Script.Gameplay
{
    public static class RopeUtils
    {
        // Проверка пересечения двух отрезков
        public static bool AreSegmentsIntersecting(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
        {
            float d1 = Direction(q1, q2, p1);
            float d2 = Direction(q1, q2, p2);
            float d3 = Direction(p1, p2, q1);
            float d4 = Direction(p1, p2, q2);

            // Если отрезки пересекаются
            return (d1 > 0 && d2 < 0 || d1 < 0 && d2 > 0) && (d3 > 0 && d4 < 0 || d3 < 0 && d4 > 0);
        }

        // Направление (для определения, лежат ли точки по одну сторону от отрезка)
        private static float Direction(Vector2 pi, Vector2 pj, Vector2 pk)
        {
            return (pk.x - pi.x) * (pj.y - pi.y) - (pj.x - pi.x) * (pk.y - pi.y);
        }
    }
}