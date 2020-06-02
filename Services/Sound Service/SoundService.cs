using UnityEngine;
using System.Collections.Generic;

namespace CatAstropheGames
{
    public class SoundService : ISoundService
    {
        private static readonly Dictionary<AudioClip, OneSound> sounds = new Dictionary<AudioClip, OneSound>();
        private bool muted;

        public void Play(AudioClip clip)
        {
            if (!muted)
            {
                float volume = 1f;
                if (clip != null)
                {
                    if (!sounds.ContainsKey(clip))
                    {
                        sounds.Add(clip, new OneSound(clip, volume));
                    }

                    sounds[clip].PlayAnother();
                }
                else
                {
                    Debug.LogWarning("Clip to be played shouldn't be null, but here we are.");
                }
            }
        }

        public void SwitchToBackground()
        {
            //we mute, because right now background sounds result in fatal error
            muted = true;
        }

        public void SwitchToForeground()
        {
            muted = false;
        }
    }

    public class OneSound
    {
        private readonly AudioClip audioClip;
        private readonly float volume;

        public OneSound(AudioClip ac, float volume)
        {
            audioClip = ac;
            this.volume = volume;
        }

        public void PlayAnother()
        {
            GameObject go = new GameObject("sound clip: " + audioClip.name);
            AudioSource audio = go.AddComponent<AudioSource>();
            audio.volume = volume;
            audio.clip = audioClip;
            audio.spatialBlend = -1f;
            audio.Play();
            go.AddComponent<DestroyIn>().Initialize(audioClip.length);
        }
    }
}