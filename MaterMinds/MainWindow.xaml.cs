using MaterMinds.View;
using System.Windows;
using System.Windows.Media;

namespace MaterMinds
{
    public partial class MainWindow : Window
    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            var view = new MainMenuView();
            Main.Content = view;
        }
    }
}
