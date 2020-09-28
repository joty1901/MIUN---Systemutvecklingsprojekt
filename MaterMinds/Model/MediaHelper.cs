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
        public static bool first;
        public static double volume = 1;
        public static bool IsMuted;

        public static void Mute()
        {
            _backgroundPlayer.Stop();
            _soundEffectPlayer.Stop();
            _soundEffectPlayer.Volume = volume;
            if(IsMuted == false)
            {
                IsMuted = true;
            }
            else
            {
                IsMuted = false;
                _backgroundPlayer.IsMuted = IsMuted;
                _soundEffectPlayer.IsMuted = IsMuted;
            }
        }

        

        public static void Start(MediaPlayer m, Uri u)
        {
            if(m == _backgroundPlayer)
            {
                _backgroundPlayer.Open(u);
                _backgroundPlayer.IsMuted = IsMuted;
                _backgroundPlayer.Play();
            }
            else
            {
                _soundEffectPlayer.Open(u);
                _soundEffectPlayer.IsMuted = IsMuted;
                _backgroundPlayer.Play();
            }

            
            //else
            //{
            //    SoundEffectPlayer = m;
            //    m.Open(u);
            //    m.Play();
            //}
            
        }
    }
}
