using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterMinds
{
    /// <summary>
    /// Interaction logic for OrangePeg.xaml
    /// </summary>
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
