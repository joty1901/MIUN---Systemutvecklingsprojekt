using System.Windows;
using System.Windows.Controls;

namespace MaterMinds
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private GameViewModel model;
        public GamePage()
        {
            InitializeComponent();
            model = new GameViewModel();
            DataContext = model;
        }

        private void ButtonYellow_Click(object sender, RoutedEventArgs e)
        {
            //var page = new MainMenuePage();
            //Main.Content = page;
        }
    }
}
