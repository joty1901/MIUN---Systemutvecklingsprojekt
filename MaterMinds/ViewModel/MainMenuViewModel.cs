using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MaterMinds.ViewModel
{
    class MainMenuViewModel : BaseViewModel
    {
        public ICommand ChoosePlayer { get; set; }
        public ICommand ViewHighscore { get; set; }
        public ICommand NewGame { get; set; }

        private MainWindow Main = (MainWindow)Application.Current.MainWindow;

        public MainMenuViewModel()
        {
            ChoosePlayer = new RelayCommand(ChoosePlayerPage);
            ViewHighscore = new RelayCommand(HighscorePage);
            NewGame = new RelayCommand(NewGamePage);
        }

        private void ChoosePlayerPage()
        {
           Main.Content = new ChoosePlayerPage();
        }

        private void HighscorePage()
        {
            Main.Content = new HighscorePage();
        }

        private void NewGamePage()
        {
            Main.Content = new GamePage();
        }
    }
}
