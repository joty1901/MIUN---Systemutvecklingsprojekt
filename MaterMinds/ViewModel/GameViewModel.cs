using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace MaterMinds
{
    class GameViewModel : BaseViewModel
    {
        public Dictionary<int, int> PlacedPegs { get; set; } = new Dictionary<int, int>();
        //public PegPosition Peg { get; set; } = PegPosition.NewValue;
        public ObservableCollection<int> MyProperty { get; set; }
        public ObservableCollection<bool> IsActive { get; set; } = new ObservableCollection<bool> { true, false, false, false, false, false, false };
        public ICommand BoolChecker { get; set; }
        public int Counter { get; set; } = 1;
        private readonly MediaPlayer mediaPlayer = new MediaPlayer();

        public GameViewModel()
        {
            PlaySound();
            BoolChecker = new RelayCommand(CheckBool);
        }


        public void CheckBool()
        {
            if (Counter <= 6)
            {
                IsActive = new ObservableCollection<bool> { false, false, false, false, false, false, false};
                IsActive[Counter] = true;
                Counter++;
            }


        }

        public void PlaySound()
        {
            
            mediaPlayer.Open(new Uri(@"Sound/Rumble.mp3", UriKind.Relative));
            mediaPlayer.Play();
        }
    }
}
