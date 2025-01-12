using System;
using _Script.Gameplay.WinSystem.Checker;
using _Script.Infrastructure.Data.StaticData;

namespace _Script.Gameplay.ScoreSystem
{
    public class ScorePresenter : IScorePresenter, IDisposable 
    {
        private readonly IWinService _winService;
        private readonly IScoreService _scoreService;
        private readonly IStaticDataProvider _staticDataProvider;

        private IScoresView _scoresView;
        
        public ScorePresenter(IWinService winService,
            IScoreService scoreService,
            IStaticDataProvider staticDataProvider)
        {
            _winService = winService;
            _scoreService = scoreService;
            _staticDataProvider = staticDataProvider;
        }

        public void Initialize(IScoresView scoresView)
        {
            _scoresView = scoresView;
            
            _winService.OnWin += AddScores;
            _scoreService.ChangeScores += _scoresView.ChangeScores;
        }

        private void AddScores() => 
            _scoreService.TryAddScores(_staticDataProvider.ScoresConfig.WinScoreValue);

        public void Dispose()
        {
            _winService.OnWin -= AddScores;
            _scoreService.ChangeScores -= _scoresView.ChangeScores;
        }
    }

    public interface IScorePresenter
    {
        void Initialize(IScoresView scoresView);
    }
}