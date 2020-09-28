using MaterMinds.Command;
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
    public class BaseViewModel : INotifyPropertyChanged, INavigation, IMedia
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
            //MediaHelper.Mute();
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
            m.Open(u);
            m.Volume = Volume;
            m.Play(); 
        }

        public void Mute(object parameter)
        {
            if (!BackgroundPlayer.IsMuted || !SoundEffectPlayer.IsMuted)
            {
                BackgroundPlayer.IsMuted = true;
                SoundEffectPlayer.IsMuted = true;
                Volume = 0;
            }
            else
            {
                BackgroundPlayer.IsMuted = false;
                SoundEffectPlayer.IsMuted = false;
                Volume = 1;
            }
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
