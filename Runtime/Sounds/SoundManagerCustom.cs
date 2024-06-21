using System;
using GiangCustom.DesignPattern;
using UnityEngine;

namespace GiangCustom.Runtime.Sounds
{
    
    [Serializable]
    public enum SoundName
    {
        Confetti,
        Tap,
        Win,
        Lose,
        Hit,
        Hit1,
        Hit2,
        Hint,
        Move,
        Walk,
        Walk1,
        BgSoundMain,
        BgSoundGameplay,
        Drawing1,
        Drawing2,
        Combat,
        GreenAppear1,
        GreenAppear2,
        GreenWalk1,
        GreenWalk2,
        OrangeAppear1,
        OrangeAppear2,
        OrangeWalk1,
        OrangeWalk2,
        BlueAppear1,
        BlueAppear2,
        BlueWalk1,
        BlueWalk2
    }

    [Serializable]
    public class Sound
    {
        public SoundName soundName;
        public AudioClip audioClip;
    }
    public class SoundManagerCustom : SingletonMonoBehaviour<SoundManagerCustom>
    {
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource sfx;
        [SerializeField] private Sound[] sounds;

        private void Start()
        {
            if (music)
            {
                if (!PlayerPrefsManager.BGSound)
                {
                    music.Stop();
                }
            }
        }

        public void PlaySoundLoop(SoundName soundName, bool isStop = false)
        {
            // Debug.LogError("play sound loop: " + soundName);
            if (isStop)
            {
                sfx.Stop();
                return;
            }
            if (!PlayerPrefsManager.Sound)
            {
                if (sfx.isPlaying)
                {
                    sfx.Stop();
                }
                return;
            }

            var s = Array.Find(sounds, s => s.soundName == soundName);
            if (s == default) return;
            sfx.clip = s.audioClip;
            // Debug.LogError("play sound loop 2: " + soundName);
            sfx.loop = true;
            sfx.Play();
        }

        public void PlaySound(SoundName soundName)
        {
            if (!PlayerPrefsManager.Sound)
            {
                if (sfx.isPlaying)
                {
                    sfx.Stop();
                }

                return;
            }
            sfx.Stop();
            sfx.loop = false;
            sfx.clip = null;
            var s = Array.Find(sounds, s => s.soundName == soundName);
            if (s == default) return;
            sfx.PlayOneShot(s.audioClip);
        }

        public void UpdateBGSound()
        {
            if (PlayerPrefsManager.BGSound)
            {
                music.Play();
            }
            else
            {
                music.Stop();
            }
        }
        
        public void ChangeBgSound(SoundName soundName)
        {
            music.clip = Array.Find(sounds, s => s.soundName == soundName).audioClip;
            if (PlayerPrefsManager.BGSound)
            {
                music.Play();
            }
            else
            {
                music.Stop();
            }
        }

        public Sound GetAudioClip(SoundName soundName)
        {
            return Array.Find(sounds, s => s.soundName == soundName);
        }
    }
}