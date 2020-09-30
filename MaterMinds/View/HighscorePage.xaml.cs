using System.Windows.Controls;

namespace MaterMinds
{
    public partial class HighscorePage : Page
    {
        public HighscorePage()
        {
            InitializeComponent();
            DataContext = new HighscoreViewModel();
        }
    }
}
