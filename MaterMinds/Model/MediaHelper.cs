using System;
using System.Collections.Generic;
using System.Text;
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
            //_soundEffectPlayer.Volume = volume;
            if(Muted == false)
            {
                Muted = true;
                _backgroundPlayer.IsMuted = Muted;
                _soundEffectPlayer.IsMuted = Muted;
                //_backgroundPlayer.Stop();
                //_soundEffectPlayer.Stop();
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
