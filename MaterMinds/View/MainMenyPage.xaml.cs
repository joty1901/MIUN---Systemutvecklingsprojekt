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
    /// Interaction logic for MainMenyPage.xaml
    /// </summary>
    public partial class MainMenyPage : Page
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public MainMenyPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var page = new GamePage();
            MainMeny.Content = page;
            mediaPlayer.Open(new Uri(@"Resources/Sound/Rumble.mp3", UriKind.Relative));
            mediaPlayer.Play();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChoosePlayerPage page = new ChoosePlayerPage();
            MainMeny.Content = page;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HighscorePage page = new HighscorePage();
            MainMeny.Content = page;
        }
    }
}
