using UnityEngine;

namespace CatAstropheGames
{
    public interface ISoundService
    {
        void Play(AudioClip clip);
        void SwitchToForeground();
        void SwitchToBackground();
    }
}