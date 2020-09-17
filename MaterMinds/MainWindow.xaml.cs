using MaterMinds.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MaterMinds.Repository;

namespace MaterMinds
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer mediaPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
            var view = new MainMenuView();
            Main.Content = view;
            playBackground();
        }

        public void playBackground()
        {
            mediaPlayer.Open(new Uri(@"Resources/Sound/Spacemusic.mp3", UriKind.Relative));
            mediaPlayer.Play();
            mediaPlayer.MediaEnded += new EventHandler(Media_Ended);
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            playBackground();
        }
    }
}
