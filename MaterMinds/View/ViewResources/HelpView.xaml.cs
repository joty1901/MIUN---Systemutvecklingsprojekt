using System.Windows.Controls;

namespace MaterMinds
{
    public partial class HelpView : UserControl
    {
        public HelpView()
        {
            InitializeComponent();
            ReadText();
        }

        private void ReadText()
        {
            HelpText.Text = System.IO.File.ReadAllText("Resources/Text/Rules.txt"); 
        }
    }
}
