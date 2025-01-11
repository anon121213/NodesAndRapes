using System;

namespace _Script.Gameplay.ScoreSystem
{
    public class ScoreService : IScoreService
    {
        public event Action<int> ChangeScores;
        
        private int _scores;
        
        public void TryAddScores(int scores)
        {
            if (scores <= 0)
                return;
            
            _scores += scores;
            ChangeScores?.Invoke(_scores);
        }

    }

    public interface IScoreService
    {
        void TryAddScores(int scores);
        event Action<int> ChangeScores;
    }
}