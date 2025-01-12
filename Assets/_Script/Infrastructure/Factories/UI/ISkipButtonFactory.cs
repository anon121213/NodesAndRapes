using _Script.Gameplay.SkipButton;

namespace _Script.Infrastructure.Factories.UI
{
    public interface ISkipButtonFactory
    {
        void InitializeSkipButton(ISkipView skipView);
    }
}