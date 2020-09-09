using MaterMinds.Input;
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
using static MaterMinds.IRepository;

namespace MaterMinds
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOne_Click(object sender, RoutedEventArgs e)
        {
            //User user = new User();
            //user.Nickname = "Espen";
            //Score score = new Score(user, 1336);

            //AddUserWithScore(score, user);


            //Test to see if you can get top 10 highscores of a specific user
            //List<User> t;
            //t = GetPlayers().ToList();
            //GetUserHighscore(t[1]);
            

            //Testing to see if you can get top 10 scores of all time
            //GetTopTen();
        }

        private void BtnTwo_Click(object sender, RoutedEventArgs e)
        {
            ChoosePlayerPage page = new ChoosePlayerPage();
            Main.Content = page;
        }

        private void BtnThree_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
