﻿using MaterMinds.View;
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
        public ICommand ChoosePlayer { get; set; }
        public ICommand ExitGame { get; set; }


        

        public MainMenuViewModel()
        {
            ChoosePlayer = new RelayCommand(ChoosePlayerPage);
            ViewTopHighscore = new RelayCommand(GetHighscorePage);
            ExitGame = new RelayCommand(CloseApplication);
            MuteCommand = new RelayCommand(Mute);
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
