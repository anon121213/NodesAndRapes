using _Script.Gameplay.ScoreSystem;
using UnityEngine;

namespace _Script.Infrastructure.Factories.UI
{
    public class ScoresFactory : IScoresFactory
    {
        private readonly IScorePresenter _scoresPresenter;

        private GameObject _scoreWindow;
        
        public ScoresFactory(IScorePresenter scoresPresenter) => 
            _scoresPresenter = scoresPresenter;

        public void InitializeScoreWindow(IScoresView scoresView) => 
            _scoresPresenter.Initialize(scoresView);
    }
}