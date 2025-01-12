using System.Collections.Generic;
using UnityEngine;

namespace _Script.Gameplay.SoundSystem.Data
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Data/Configs/SoundConfig")]
    public class SoundConfig : ScriptableObject
    {
        [field: SerializeField] public List<Sound> Sounds { get; private set; } = new ();
    }
}