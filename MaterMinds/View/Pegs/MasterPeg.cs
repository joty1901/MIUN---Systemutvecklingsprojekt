using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace MaterMinds
{
    public class MasterPeg : UserControl
    {
        public int ColorIndex { get; set; }

        public MasterPeg()
        {
            Height = 30;
            Name = "Peg";
            Width = 30;
        }
    }
}
