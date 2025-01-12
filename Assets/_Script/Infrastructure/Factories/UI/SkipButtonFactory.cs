using _Script.Gameplay.SkipButton;
using UnityEngine;

namespace _Script.Infrastructure.Factories.UI
{
    public class SkipButtonFactory : ISkipButtonFactory
    {
        private readonly ISkipPresenter _skipPresenter;
        private readonly Canvas _canvas;

        private GameObject _skipButton;
        
        public SkipButtonFactory(ISkipPresenter skipPresenter) => 
            _skipPresenter = skipPresenter;

        public void InitializeSkipButton(ISkipView skipView) => 
            _skipPresenter.Initialize(skipView);
    }
}