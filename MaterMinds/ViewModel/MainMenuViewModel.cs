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
        public ICommand AlterMuteButton { get; set; }

        public MainMenuViewModel()
        {
            ChoosePlayerCommand = new RelayCommand(ChoosePlayerPage, AlwaysTrue);
            ViewTopHighscoreCommand = new RelayCommand(GetHighscorePage, AlwaysTrue);
            ExitGameCommand = new RelayCommand(CloseApplication, AlwaysTrue);
            MuteCommand = new RelayCommand(Mute, AlwaysTrue);
            StartDBConnection();
        }

        private void ChoosePlayerPage(object parameter)
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
