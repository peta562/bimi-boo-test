using UnityEngine;

namespace Infrastructure.Services.SoundManager {
    public sealed class SoundManager : ISoundManager {
        const string SoundHolderPath = "SoundHolder";

        readonly SoundHolder _soundHolder;
        readonly AudioSource _audioSource;

        public SoundManager() {
            _soundHolder = LoadSoundHolder();
            _audioSource = CreateAudioSource();

            ConfigureAudioSource();
        }

        public void PlaySound(SoundType soundType) {
            var audioClip = GetAudioClip(soundType);

            if ( audioClip == null ) {
                Debug.LogError($"Can't find audio clip for {soundType.ToString()}");
                return;
            }

            if ( _audioSource.isPlaying ) {
                _audioSource.Stop();
            }

            _audioSource.PlayOneShot(audioClip);
        }

        AudioClip GetAudioClip(SoundType soundType) {
            foreach (var sound in _soundHolder.Sounds) {
                if ( sound.SoundType == soundType ) {
                    return sound.AudioClip;
                }
            }

            return null;
        }

        SoundHolder LoadSoundHolder() {
            var soundHolder = Resources.Load<SoundHolder>(SoundHolderPath);

            if ( soundHolder == null ) {
                Debug.LogError("There are no SoundHolder in Resources folder");
                return null;
            }

            return soundHolder;
        }

        AudioSource CreateAudioSource() {
            var gameObject = new GameObject("AudioSource");
            var audioSource = gameObject.AddComponent<AudioSource>();

            return audioSource;
        }

        void ConfigureAudioSource() {
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
            _audioSource.volume = 0.6f;
        }
    }
}