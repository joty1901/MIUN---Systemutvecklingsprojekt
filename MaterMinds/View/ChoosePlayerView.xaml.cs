using System.Windows.Controls;

namespace MaterMinds.View
{
    public partial class ChoosePlayerView : UserControl
    {
        public ChoosePlayerView()
        {
            InitializeComponent();
            DataContext = new ChoosePlayerViewModel();
        }
    }
}
