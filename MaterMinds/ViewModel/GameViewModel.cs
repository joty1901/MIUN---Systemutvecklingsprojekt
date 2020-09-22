using MaterMinds.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;
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
        public ICommand NextRoundCommand { get; set; }
        public ICommand ResetGame { get; set; }
        public ICommand Help { get; set; }
        public int Rounds { get; set; } = 0;
        private readonly MediaPlayer mediaPlayer = new MediaPlayer();
        public ObservableCollection<string[]> hintArray { get; set; } = new ObservableCollection<string[]>();
        public ObservableCollection<string> CorrectAnswerArray { get; set; } = new ObservableCollection<string>();
        public string IsHidden { get; set; }
        public string IsSuperHidden { get; set; }
        public ObservableCollection<string> BackgroundColor { get; set; } = new ObservableCollection<string> { "White", "Transparent", "Transparent", "Transparent", "Transparent", "Transparent", "Transparent" };
        public int GameTimerInSecounds { get; set; }
        public int GameTimerInMinutes { get; set; }
        public string GameTimer { get; set; }
        public int Score { get; set; }
        public Player Player { get; set; }
        public ObservableCollection<string> WinOrLoss { get; set; } = new ObservableCollection<string> { " ", " " };


        public GameViewModel(Player player)
        {
            PlaySound();
            game = new GameEngine();
            Player = player;
            NextRoundCommand = new RelayCommand(NextRound);
            Back = new RelayCommand(GetBack);
            ResetGame = new RelayCommand(RestartGame);
            Help = new RelayCommand(GetHelp);
            StartTimer();
        }


        public void AddScoreToDB()
        {
            Repository.AddPlayerScore(Player.Id, Score);
        }

        public void NextRound()
        {
            
            if (PlacedPegs.Count != 0)
            {
                if (game.CheckWinCon(PlacedPegs))
                {
                    EndGame(true);
                }
                else 
                {
                    if (Rounds < 6)
                    {
                        UpdateGameBoard();
                    }
                    /*
                     * This is the loose condition
                     */
                    else
                    {
                        EndGame(false);
                    }
                }
                hintArray.Add(game.CheckPegPosition(PlacedPegs));
                PlacedPegs.Clear();
            }
            else
            {
                MessageBox.Show($"{Player.Nickname} du måste lägga minst en peg på spelplanen");
            }
        }
        public void UpdateGameBoard()
        {
            IsActive[Rounds] = false;
            BackgroundColor[Rounds] = "Transparent";
            Rounds++;
            IsActive[Rounds] = true;
            BackgroundColor[Rounds] = "White";
        }
        public void EndGame(bool Win)
        {
            StopTimer();
            GetAnswer();
            IsHidden = "Visible";
            if (Win)
            {
                Score = game.CalcScore(Rounds, GameTimerInSecounds, GameTimerInMinutes);
                AddScoreToDB();
                WinOrLoss[0] = "Visible";
            }
            else
            {
                IsActive = new ObservableCollection<bool> { false, false, false, false, false, false, false };
                WinOrLoss[1] = "Visible";
            }
        }
        public void GetAnswer()
        {
            foreach (var c in game.GetCorrectAnswer())
            {
                switch (c.Value)
                {
                    case 1:
                        CorrectAnswerArray.Add("Red");
                        break;
                    case 2:
                        CorrectAnswerArray.Add("Yellow");
                        break;
                    case 3:
                        CorrectAnswerArray.Add("Green");
                        break;
                    case 4:
                        CorrectAnswerArray.Add("Blue");
                        break;
                    case 5:
                        CorrectAnswerArray.Add("Purple");
                        break;
                    case 6:
                        CorrectAnswerArray.Add("Orange");
                        break;
                }
            }
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
        private void GetHelp()
        {
            if (IsSuperHidden != "Visible")
            {
                IsSuperHidden = "Visible";
            }
            else
                IsSuperHidden = "Hidden";
        }
    }
}
