using System.Windows.Media;
using System.Windows.Shapes;

namespace MaterMinds
{
    public partial class PurplePeg : MasterPeg
    {
        public PurplePeg()
        {
            InitializeComponent();
            CreatePeg();
        }

        private void CreatePeg()
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = Brushes.Purple,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            ColorIndex = (int)PegColor.Purple;
            this.Color = Brushes.Purple;

            master.Children.Add(ellipse);
        }
    }
}
