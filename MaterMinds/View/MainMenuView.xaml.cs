using MaterMinds.Model;
using MaterMinds.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterMinds.View
{
    /// <summary>
    /// Interaction logic for MainMenyView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        private MainMenuViewModel model;
        public MainMenuView()
        {
            InitializeComponent();
            model = new MainMenuViewModel();
            DataContext = model;
            PlayBackground();
        }

        private void PlayBackground()
        {
            MediaHelper.PlayMedia(MediaHelper._backgroundPlayer, new Uri(@"Resources/Sound/Spacemusic.mp3", UriKind.Relative));
        }

    }
}
