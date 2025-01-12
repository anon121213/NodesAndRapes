using System;
using UnityEngine.AddressableAssets;

namespace _Script.Gameplay.SoundSystem.Data
{
    [Serializable]
    public struct Sound
    {
        public AssetReference AudioClip;
        public SoundType SoundType;
        public SoundGroup SoundGroup;
    }

    public enum SoundGroup
    {
        BackgroundSound = 0,
        LoopedSounds = 1,
        OnceSound = 2
    }
}