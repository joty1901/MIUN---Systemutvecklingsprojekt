using MaterMinds.View;
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
        GameEngine game;
        public Dictionary<int, int> PlacedPegs { get; set; } = new Dictionary<int, int>();
        public ObservableCollection<int> HintToAnswer { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<bool> IsActive { get; set; } = new ObservableCollection<bool> { true, false, false, false, false, false, false };
        public ICommand BoolChecker { get; set; }
        public int Counter { get; set; } = 0;
        private readonly MediaPlayer mediaPlayer = new MediaPlayer();
        public ObservableCollection<string[]> plojArray { get; set; } = new ObservableCollection<string[]>();
        public ObservableCollection<string> CorrectAnswerArray { get; set; } = new ObservableCollection<string>();
        public string IsHidden { get; set; }
        public ObservableCollection<string> BackgroundColor { get; set; } = new ObservableCollection<string> { "LightGray", "Gray", "Gray", "Gray", "Gray", "Gray", "Gray"};
    //public PegColor


    public GameViewModel()
        {
            game = new GameEngine();
            PlaySound();
            BoolChecker = new RelayCommand(CheckBool);
            Back = new RelayCommand(GetBack);
            
        }
        
        public void CheckBool()
        {
            game.CheckWinCon(PlacedPegs);
            if (game.WinCondition)
            {
                GetAnswer();
            }
            else
            {
                if (Counter < 6)
                {
                    IsActive[Counter] = false;
                    BackgroundColor[Counter] = "Gray";
                    Counter++;
                    IsActive[Counter] = true;
                    BackgroundColor[Counter] = "LightGray";
                }
                else
                {
                    IsActive = new ObservableCollection<bool> { false, false, false, false, false, false, false };
                    GetAnswer();
                }
            }
            plojArray.Add(game.CheckPegPosition(PlacedPegs));
            PlacedPegs.Clear();
        }
        public void GetAnswer()
        {
            Dictionary<int, int> answer = game.GetCorrectAnswer();
            foreach (var c in answer)
            {
                if (c.Value == 1)
                {
                    CorrectAnswerArray.Add("Red");
                }
                else if(c.Value == 2)
                {
                    CorrectAnswerArray.Add("Yellow");
                }
                else if (c.Value == 3)
                {
                    CorrectAnswerArray.Add("Green");
                }
                else if (c.Value == 4)
                {
                    CorrectAnswerArray.Add("Blue");
                }
                else if (c.Value == 5)
                {
                    CorrectAnswerArray.Add("Purple");
                }
                else if (c.Value == 6)
                {
                    CorrectAnswerArray.Add("Orange");
                }
            }
            IsHidden = "Visible";
        }

        public void PlaySound()
        {
            //Comented out for sanity purposes during testing
            //mediaPlayer.Open(new Uri(@"Resources/Sound/Rumble.mp3", UriKind.Relative));
            //mediaPlayer.Play();
        }
    }
}
