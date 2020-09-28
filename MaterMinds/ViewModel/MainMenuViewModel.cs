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
            ChoosePlayerCommand = new RelayCommand(ChoosePlayerPage);
            ViewTopHighscoreCommand = new RelayCommand(GetHighscorePage);
            ExitGameCommand = new RelayCommand(CloseApplication);
            MuteCommand = new RelayCommand(MediaHelper.Mute);
            StartDBConnection();
        }

        private void ChoosePlayerPage()
        {
            Main.Content = new ChoosePlayerView();
        }

        private void StartDBConnection()
        {
            Repository.StartDb();
        }

        private void CloseApplication()
        {
            Main.Close();
        }

       

    }
}
