using MaterMinds.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MaterMinds
{
    public class GameViewModel : BaseViewModel
    {
        GameEngine game;
        DispatcherTimer timer;

        public Dictionary<int, int> PlacedPegs { get; set; } = new Dictionary<int, int>();
        public ObservableCollection<int> HintToAnswer { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<bool> IsActive { get; set; } = new ObservableCollection<bool> { true, false, false, false, false, false, false };
        public ICommand BoolChecker { get; set; }
        public ICommand ResetGame { get; set; }
        public int Counter { get; set; } = 0;
        private readonly MediaPlayer mediaPlayer = new MediaPlayer();
        public ObservableCollection<string[]> hintArray { get; set; } = new ObservableCollection<string[]>();
        public ObservableCollection<string> CorrectAnswerArray { get; set; } = new ObservableCollection<string>();
        public string IsHidden { get; set; }
        public ObservableCollection<string> BackgroundColor { get; set; } = new ObservableCollection<string> { "White", "Transparent", "Transparent", "Transparent", "Transparent", "Transparent", "Transparent" };
        public int GameTimerInSecounds { get; set; }
        public int GameTimerInMinutes { get; set; }
        public string GameTimer { get; set; }
        public int Score { get; set; }
        public Player Player { get; set; }

        public GameViewModel(Player player)
        {
            PlaySound();
            game = new GameEngine();
            Player = player;
            BoolChecker = new RelayCommand(CheckBool);
            Back = new RelayCommand(GetBack);
            ResetGame = new RelayCommand(RestartGame);
            StartTimer();
        }

        public void SetScore(Player player, int points)
        {
            int value = game.CalcScore(Counter, GameTimerInSecounds, GameTimerInMinutes);
            Score score = new Score(player, value);
        }

        public void CheckBool()
        {
            
            if (PlacedPegs.Count != 0)
            {
                game.CheckWinCon(PlacedPegs);


                if (game.WinCondition)
                {
                    Score = game.CalcScore(Counter, GameTimerInSecounds, GameTimerInMinutes);
                    SetScore(Player, Score);
                    StopTimer();
                    GetAnswer();
                }
                else 
                {
                    if (Counter < 6)
                    {
                        IsActive[Counter] = false;
                        BackgroundColor[Counter] = "Transparent";
                        Counter++;
                        IsActive[Counter] = true;
                        BackgroundColor[Counter] = "White";
                    }
                    else
                    {
                        IsActive = new ObservableCollection<bool> { false, false, false, false, false, false, false };
                        GetAnswer();
                        StopTimer();
                    }
                }
                hintArray.Add(game.CheckPegPosition(PlacedPegs));
                PlacedPegs.Clear();
            }
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
        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void StopTimer()
        {
            timer.Stop();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (GameTimerInSecounds == 59)
            {
                GameTimerInMinutes++;
                GameTimerInSecounds = 0;
            }
            else
            {
                GameTimerInSecounds++;
            }
            if (GameTimerInSecounds < 10 && GameTimerInMinutes < 10)
            {
                GameTimer = $"0{GameTimerInMinutes}:0{GameTimerInSecounds}";
            }
            else if(GameTimerInMinutes >= 10 && GameTimerInSecounds >= 10)
            {
                GameTimer = $"{GameTimerInMinutes}:{GameTimerInSecounds}";
            }
            else if (GameTimerInMinutes >= 10 && GameTimerInSecounds < 10)
            {
                GameTimer = $"{GameTimerInMinutes}:0{GameTimerInSecounds}";
            }
            else
            {
                GameTimer = $"0{GameTimerInMinutes}:{GameTimerInSecounds}";
            }
        }
        public void RestartGame()
        {
            Main.Content = new GamePage(Player);
        }
    }
}
