using System.Collections.Generic;
using System.Linq;
using _Script.Gameplay.SoundSystem.Data;
using _Script.Infrastructure.Data.AddressableLoader;
using _Script.Infrastructure.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Gameplay.SoundSystem
{
    public class SoundService : ISoundService
    {
        private readonly Dictionary<SoundType, Sound> _sounds = new();
        private readonly List<SoundSource> _sources;
        
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IAddressablesLoader _addressablesLoader;

        public SoundService(IStaticDataProvider staticDataProvider,
            IAddressablesLoader addressablesLoader,
            List<SoundSource> source)
        {
            _staticDataProvider = staticDataProvider;
            _addressablesLoader = addressablesLoader;
            _sources = source;
        }
        
        public void AddSounds(List<Sound> newSounds)
        {
            foreach (var sound in newSounds)
                _sounds.Add(sound.SoundType, sound);
        }

        public async UniTask PlayLoopAudio(SoundType soundType)
        {
            Sound sound = _sounds[soundType];

            foreach (var source in _sources.Where(source => source.SoundGroup == sound.SoundGroup))
            {
                var audio = await _addressablesLoader.LoadAsync<AudioClip>(sound.AudioClip);
                PlayLoopSound(audio, source.AudioSource);
            }
        }
        
        public async UniTask PlayOnceSound(SoundType soundType)
        {
            Sound sound = _sounds[soundType];

            foreach (var source in _sources.Where(source => source.SoundGroup == sound.SoundGroup))
            {
                var audio = await _addressablesLoader.LoadAsync<AudioClip>(sound.AudioClip);
                source.AudioSource.PlayOneShot(audio);
            }
        }
        
        private void PlayLoopSound(AudioClip clip,
            AudioSource source)
        {
            source.clip = clip;
            source.loop = true;
            source.Play();
        }

        public void StopSounds()
        {
            foreach (var source in _sources)
            {
                source.AudioSource.loop = false;
                source.AudioSource.clip = null;
            }
        }
    }
}