using System.Windows.Media;
using System.Windows.Shapes;

namespace MaterMinds
{
    public partial class YellowPeg : MasterPeg
    {
        public YellowPeg()
        {
            InitializeComponent();
            CreatePeg();
        }

        private void CreatePeg()
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            ColorIndex = (int)PegColor.Yellow;
            this.Color = Brushes.Yellow;

            master.Children.Add(ellipse);
        }
    }
}
