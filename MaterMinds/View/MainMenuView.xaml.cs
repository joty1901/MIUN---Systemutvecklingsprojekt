using MaterMinds.Model;
using MaterMinds.ViewModel;
using System;
using System.Windows.Controls;

namespace MaterMinds.View
{
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
