﻿using MaterMinds.Command;
using MaterMinds.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace MaterMinds
{
    public class BaseViewModel : INotifyPropertyChanged, INavigation, IMedia
    {

        public MainWindow Main = (MainWindow)Application.Current.MainWindow;

        public ICommand MainMenuCommand { get ; set; }
        public ICommand ViewTopHighscoreCommand { get; set; }
        public ICommand ViewTopFrequentPlayersCommand { get; set; }
        public MediaPlayer BackgroundPlayer { get; set; } = new MediaPlayer();
        public MediaPlayer SoundEffectPlayer { get; set; } = new MediaPlayer();
        public ICommand MuteCommand { get; set; }
        public double Volume { get; set; } = 1;

        #region NavigationMethods
        public void GetMainMenuView()
        {
            Main.Content = new MainMenuView();
        }

        public void GetHighscorePage()
        {
            Main.Content = new HighscorePage();
        }
        #endregion

        #region MediaMethods
        public void Start(MediaPlayer m, Uri u)
        {
            m.Open(u);
            m.Volume = Volume;
            m.Play(); 
        }

        public void Mute()
        {
           BackgroundPlayer.IsMuted = true;
            
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
