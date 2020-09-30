using System;
using System.Windows.Media;

namespace MaterMinds.Model
{
    static class MediaHelper 
    {
        public static readonly MediaPlayer _backgroundPlayer = new MediaPlayer();
        public static readonly MediaPlayer _soundEffectPlayer = new MediaPlayer();
        public static double volume = 1;
        public static bool Muted;

        public static void Mute()
        {
            if(Muted == false)
            {
                Muted = true;
                _backgroundPlayer.IsMuted = Muted;
                _soundEffectPlayer.IsMuted = Muted;
            }
            else
            {
                Muted = false;
                _backgroundPlayer.IsMuted = Muted;
                _soundEffectPlayer.IsMuted = Muted;
            }
        }

        public static void PlayMedia(MediaPlayer m, Uri u)
        {
            if(m == _backgroundPlayer)
            {
                _backgroundPlayer.Open(u);
                _backgroundPlayer.IsMuted = Muted;
                _backgroundPlayer.Play();
            }
            else
            {
                _soundEffectPlayer.Open(u);
                _soundEffectPlayer.IsMuted = Muted;
                _soundEffectPlayer.Play();
            }
        }
    }
}
