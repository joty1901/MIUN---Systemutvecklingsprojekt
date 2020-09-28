﻿using MaterMinds.Command;
using MaterMinds.Model;
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
    public class BaseViewModel : INotifyPropertyChanged, INavigation
    {

        public MainWindow Main = (MainWindow)Application.Current.MainWindow;

        public ICommand MainMenuCommand { get ; set; }
        public ICommand ViewTopHighscoreCommand { get; set; }
        public ICommand ViewTopFrequentPlayersCommand { get; set; }
        public MediaPlayer BackgroundPlayer { get; set; } = new MediaPlayer();
        public MediaPlayer SoundEffectPlayer { get; set; } = new MediaPlayer();
        public ICommand MuteCommand { get; set; }
        public double Volume { get; set; }

        #region NavigationMethods
        public void GetMainMenuView(object parameter)
        {
            Main.Content = new MainMenuView();
        }

        public void GetHighscorePage(object parameter)
        {
            Main.Content = new HighscorePage();
        }
        #endregion

        #region MediaMethods
        public void Start(MediaPlayer m, Uri u)
        {
            MediaHelper.Start(m, u);
        }

        public void Mute(object parameter)
        {
            MediaHelper.Mute();
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual bool CanUse(object parameter)
        {
            return true;
        }

        public bool AlwaysTrue(object parameter)
        {
            return true;
        }
    }
}
