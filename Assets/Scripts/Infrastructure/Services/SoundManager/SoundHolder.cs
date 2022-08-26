using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.SoundManager {
    [Serializable]
    public class Sound {
        public SoundType SoundType;
        public AudioClip AudioClip;
    }
    [CreateAssetMenu(fileName = "SoundHolder", menuName = "SoundHolder", order = 0)]
    public sealed class SoundHolder : ScriptableObject {
        public List<Sound> Sounds;
    }
}