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
    /// Interaction logic for HelpView.xaml
    /// </summary>
    public partial class HelpView : UserControl
    {
        public HelpView()
        {
            InitializeComponent();
            ReadText();
        }

        private void ReadText()
        {
            HelpText.Text = System.IO.File.ReadAllText("Resources/Text/Rules.txt"); /*"The game randomizes a pattern of four colored pegs. Duplicates colors are allowed. The codebreaker tries to guess the pattern,in both order and color, within seven turns. Each guess is made by placing a row of colored pegs on the highlighted row in the board. Once placed, the game provides feedback by placing from zero to four hint pegs in the small box next to the highlithted row. A black hint peg is placed for each colored peg from the guess which is correct in both color and position.A white hint peg indicates the existence of a correct colored peg placed in the wrong position.If there are duplicate colours in the guess, they cannot all be awarded a hint peg unless they correspond to the same number of duplicate colours in the hidden code."*/;
                
                
        }
    }
}
