using System;
using System.Linq;
using UnityEngine;

namespace CatAstropheGames
{
    public class SoundHolder : SingletonMonoBehaviour<SoundHolder>
    {
        [SerializeField] private SoundElement[] sounds;

        public static AudioClip GetAudioClipByName(string soundName)
        {
            SoundElement soundElement = Instance.sounds.FirstOrDefault(t => t.name == soundName);
            if (soundElement != null)
            {
                return soundElement.clip;
            }

            Debug.LogWarning($"There is no sound element {soundName} in the sound holder.");
            return null;
        }
    }

    [Serializable]
    public class SoundElement
    {
        [SerializeField] public string name;
        [SerializeField] public AudioClip clip;
    }
}