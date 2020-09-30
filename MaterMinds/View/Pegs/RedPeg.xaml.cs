﻿using System.Windows.Media;
using System.Windows.Shapes;

namespace MaterMinds
{
    public partial class RedPeg : MasterPeg
    {
        LinearGradientBrush linearBrush = new LinearGradientBrush();
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
            ColorIndex = (int)PegColor.Red;
            this.Color = Brushes.Red;

            master.Children.Add(ellipse);
        }
    }
}
