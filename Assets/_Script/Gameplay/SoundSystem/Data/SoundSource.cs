using System;
using UnityEngine;

namespace _Script.Gameplay.SoundSystem.Data
{
    [Serializable]
    public struct SoundSource
    {
        public AudioSource AudioSource;
        public SoundGroup SoundGroup;
    }
}