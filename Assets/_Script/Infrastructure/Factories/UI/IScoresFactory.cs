using _Script.Gameplay.ScoreSystem;

namespace _Script.Infrastructure.Factories.UI
{
    public interface IScoresFactory
    {
        void InitializeScoreWindow(IScoresView scoresView);
    }
}