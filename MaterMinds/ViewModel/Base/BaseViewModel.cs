﻿using MaterMinds.Command;
using MaterMinds.Model;
using MaterMinds.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MaterMinds
{
    public class BaseViewModel : INotifyPropertyChanged, INavigation
    {

        public MainWindow Main = (MainWindow)Application.Current.MainWindow;
        #region Commands
        public ICommand MainMenuCommand { get ; set; }
        public ICommand ViewTopHighscoreCommand { get; set; }
        public ICommand ViewTopFrequentPlayersCommand { get; set; }
        public ICommand MuteCommand { get; set; }
        #endregion

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

        public virtual bool CeckIfCanExecute(object parameter)
        {
            return false;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
