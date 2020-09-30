using System.Windows.Media;
using System.Windows.Shapes;

namespace MaterMinds
{
    public partial class BluePeg : MasterPeg
    {
        public BluePeg()
        {
            InitializeComponent();
            CreatePeg();
        }

        private void CreatePeg()
        {
             
            Ellipse ellipse = new Ellipse
            {
                Fill = Brushes.Blue,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            ColorIndex = (int)PegColor.Blue;
            this.Color = Brushes.Blue; 
            master.Children.Add(ellipse);
        }
    }
}
