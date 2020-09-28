using MaterMinds.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Net.Http.Headers;
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

        public ICommand NextRoundCommand { get; set; }
        public ICommand ResetGameCommand { get; set; }
        public ICommand HelpCommand { get; set; }
        public Dictionary<int, int> PlacedPegs { get; set; } = new Dictionary<int, int>();
        public ObservableCollection<bool> IsActive { get; set; } = new ObservableCollection<bool> { true, false, false, false, false, false, false };
        public int Rounds { get; set; } = 0;
        public ObservableCollection<Brush[]> HintArray { get; set; } = new ObservableCollection<Brush[]>();
        public ObservableCollection<MasterPeg> CorrectAnswerArray { get; set; } = new ObservableCollection<MasterPeg>();
        public Visibility EndGameVisibility { get; set; } = Visibility.Hidden;
        public Visibility HelpViewVisibility { get; set; } = Visibility.Hidden;
        public ObservableCollection<Brush> BackgroundColor { get; set; } = new ObservableCollection<Brush> { Brushes.White, Brushes.Transparent, Brushes.Transparent, Brushes.Transparent, Brushes.Transparent, Brushes.Transparent, Brushes.Transparent };
        public int GameTimerInSecounds { get; set; } = -1;
        public int GameTimerInMinutes { get; set; }
        public int CountdownTimer { get; set; } = 3;
        public ObservableCollection<Visibility> TimerVisibility { get; set; } = new ObservableCollection<Visibility> { Visibility.Visible, Visibility.Hidden };
        public Visibility GifVisibility { get; set; }
        public string GameTimer { get; set; }
        public int Score { get; set; }
        public Player Player { get; set; }
        public ObservableCollection<Visibility> WinOrLoss { get; set; } = new ObservableCollection<Visibility> { Visibility.Hidden, Visibility.Hidden};

        public GameViewModel(Player player)
        {
            game = new GameEngine();
            Player = player;
            NextRoundCommand = new RelayCommand(NextRound);
            MainMenuCommand = new RelayCommand(GetMainMenuView);
            ResetGameCommand = new RelayCommand(RestartGame);
            ViewTopHighscoreCommand = new RelayCommand(GetHighscorePage);
            HelpCommand = new RelayCommand(SetVisibilityForHelpView);
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
                HintArray.Add(game.CheckPegPosition(PlacedPegs));
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
            BackgroundColor[Rounds] = Brushes.Transparent;
            Rounds++;
            IsActive[Rounds] = true;
            BackgroundColor[Rounds] = Brushes.White;
        }

        public void EndGame(bool win)
        {
            StopTimer();
            GetAnswer();
            GifVisibility = Visibility.Hidden;
            EndGameVisibility = Visibility.Visible;
            if (win)
            {
                Score = game.CalculateScore(Rounds, GameTimerInSecounds, GameTimerInMinutes);
                AddScoreToDB();
                WinOrLoss[0] = Visibility.Visible;
            }
            else
            {
                IsActive = new ObservableCollection<bool> { false, false, false, false, false, false, false };
                WinOrLoss[1] = Visibility.Visible;
            }
        }

        public void GetAnswer()
        {
            foreach (var c in game.GetCorrectAnswer())
            {
                switch (c.Value)
                {
                    case 1:
                        CorrectAnswerArray.Add(new RedPeg());
                        break;
                    case 2:
                        CorrectAnswerArray.Add(new YellowPeg());
                        break;
                    case 3:
                        CorrectAnswerArray.Add(new GreenPeg());
                        break;
                    case 4:
                        CorrectAnswerArray.Add(new BluePeg());
                        break;
                    case 5:
                        CorrectAnswerArray.Add(new PurplePeg());
                        break;
                    case 6:
                        CorrectAnswerArray.Add(new OrangePeg());
                        break;
                }
            }
        }

        public void PlaySound()
        {
            Start(SoundEffectPlayer, new Uri(@"Resources/Sound/WaterDrop.mp3", UriKind.Relative));
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
            
            if (CountdownTimer == 0)
            {
                TimerVisibility[0] = Visibility.Hidden;
                TimerVisibility[1] = Visibility.Visible;
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
            else
            {
                CountdownTimer--;
            }
        }

        public void RestartGame()
        {
            MessageBoxResult result = MessageBox.Show($"Do you want to restart current game?", "Warning", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Main.Content = new GamePage(Player);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void SetVisibilityForHelpView()
        {
            if (HelpViewVisibility == Visibility.Hidden)
            {
                HelpViewVisibility = Visibility.Visible;
            }
            else
                HelpViewVisibility = Visibility.Hidden;
        }
    }
}
