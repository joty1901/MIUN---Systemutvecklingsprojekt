using System.Windows.Media;
using System.Windows.Shapes;

namespace MaterMinds
{
    /// <summary>
    /// Interaction logic for GreenPeg.xaml
    /// </summary>
    public partial class GreenPeg : MasterPeg
    {
        public GreenPeg()
        {
            InitializeComponent();
            CreatePeg();
        }

        private void CreatePeg()
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = Brushes.Green,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            ColorIndex = 2;
            master.Children.Add(ellipse);
        }
    }
}
