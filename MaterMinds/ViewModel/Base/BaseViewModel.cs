using MaterMinds.Command;
using MaterMinds.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace MaterMinds
{
    public class BaseViewModel : INotifyPropertyChanged, INavigation
    {

        public MainWindow Main = (MainWindow)Application.Current.MainWindow;

        public ICommand Back { get ; set; }
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void GetBack()
        {
            Main.Content = new MainMenuView();
        }
        

    }
}
