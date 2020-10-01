using System.Windows.Media;
using System.Windows.Shapes;

namespace MaterMinds
{
    public partial class OrangePeg : MasterPeg
    {
        public OrangePeg()
        {
            InitializeComponent();
            CreatePeg();
        }

        private void CreatePeg()
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = Brushes.Orange,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            ColorIndex = (int)PegColor.Orange;
            this.Color = Brushes.Orange;

            master.Children.Add(ellipse);
        }
    }
}
