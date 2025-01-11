using TMPro;
using UnityEngine;

namespace _Script.Gameplay.ScoreSystem
{
    public class ScoresView : MonoBehaviour, IScoresView
    {
        [SerializeField] private TextMeshProUGUI _scoresText;
        
        public void ChangeScores(int value) => 
            _scoresText.text = $"Scores: {value}";
    }

    public interface IScoresView
    {
        void ChangeScores(int value);
    }
}