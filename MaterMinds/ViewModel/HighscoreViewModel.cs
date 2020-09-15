using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MaterMinds
{
    public class HighscoreViewModel : BaseViewModel
    {
        
        public Dictionary<string,int> HighscoreList { get; set; }

        public HighscoreViewModel()
        {
            GetHighscores();
        }

        public void GetHighscores()
        {
            HighscoreList = Repository.GetTopTenHigscore();
        }
    }
}
