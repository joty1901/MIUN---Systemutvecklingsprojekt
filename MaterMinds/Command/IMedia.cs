using System;
using System.Windows.Media;

namespace MaterMinds
{
    internal interface IMedia
    {
        public MediaPlayer BackgroundPlayer  { get; set; }
        public MediaPlayer SoundEffectPlayer { get; set; }

        void Start(MediaPlayer m, Uri u);
        void Mute(object parameter);
    }
}