using System.Collections.Generic;
using _Script.Gameplay.SoundSystem.Data;
using Cysharp.Threading.Tasks;

namespace _Script.Gameplay.SoundSystem
{
    public interface ISoundService
    {
        void AddSounds(List<Sound> newSounds);
        UniTask PlayLoopAudio(SoundType soundType);
        UniTask PlayOnceSound(SoundType soundType);
    }
}