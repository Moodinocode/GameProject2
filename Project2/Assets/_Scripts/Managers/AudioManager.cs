using UnityEngine;
using UnityEngine.Audio;

namespace _Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Mixer")]
        public AudioMixer mixer;

        [Header("Sources")]
        public AudioSource musicSource;
        public AudioSource uiSfxSource;        
        public AudioSource gameplaySfxSource;
    
        [Header("Sounds")]
        public AudioClip uiClickSound;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            if (!musicSource.isPlaying)
                musicSource.Play();
        }


        // Sliders use 0..1, mixer uses dB
        public void SetUISFXVolume(float value)
        {
            SetMixerVolume("UISFXVolume", value);
        }

        public void SetGameplayVolume(float value)
        {
            SetMixerVolume("GameplayVolume", value);
        }

        public void SetMusicVolume(float value)
        {
            SetMixerVolume("MusicVolume", value);
        }

        private void SetMixerVolume(string parameter, float value)
        {
            if (value <= 0.0001f)
                mixer.SetFloat(parameter, -80f);
            else
                mixer.SetFloat(parameter, Mathf.Log10(value) * 20);
        }

        public void PlayUISound(AudioClip clip)
        {
            if (clip == null || uiSfxSource == null) return;
            uiSfxSource.PlayOneShot(clip);
        }

        public void PlayGameplaySound(AudioClip clip)
        {
            if (clip == null || gameplaySfxSource == null) return;
            gameplaySfxSource.PlayOneShot(clip);
        }
    
        public void PlayButtonClick()
        {
            if (uiClickSound != null)
                uiSfxSource.PlayOneShot(uiClickSound);
        }
    }
}