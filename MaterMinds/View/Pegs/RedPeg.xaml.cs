using System.Windows.Media;
using System.Windows.Shapes;

namespace MaterMinds
{
    /// <summary>
    /// Interaction logic for RedPeg.xaml
    /// </summary>
    public partial class RedPeg : MasterPeg
    {
        public RedPeg()
        {
            InitializeComponent();
            CreatePeg();
        }

        private void CreatePeg()
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            ColorIndex = 1;
            master.Children.Add(ellipse);
        }
    }
}
