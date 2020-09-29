using MaterMinds.Model;
using MaterMinds.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MaterMinds.ViewModel
{
    class MainMenuViewModel : BaseViewModel
    {
        public ICommand ChoosePlayerCommand { get; set; }
        public ICommand ExitGameCommand { get; set; }

        public MainMenuViewModel()
        {
            ChoosePlayerCommand = new RelayCommand(GetChoosePlayerPage, CanExecute);
            ViewTopHighscoreCommand = new RelayCommand(GetHighscorePage, CanExecute);
            ExitGameCommand = new RelayCommand(CloseApplication, CanExecute);
            MuteCommand = new RelayCommand(Mute, CanExecute);
            StartDBConnection();
        }

        private void GetChoosePlayerPage(object parameter)
        {
            Main.Content = new ChoosePlayerView();
        }

        private void StartDBConnection()
        {
            Repository.StartDb();
        }

        private void CloseApplication(object parameter)
        {
            Main.Close();
        }
    }
}
