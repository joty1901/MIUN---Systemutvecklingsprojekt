using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MaterMinds
{
    public class HighscoreViewModel : BaseViewModel, IRepository
    {
        public List<string> HighscoreList { get; set; }

        public HighscoreViewModel()
        {
            GetHighscores();
        }

        public void GetHighscores()
        {
            HighscoreList = new List<string>(IRepository.GetTopTenHigscore());
        }
    }
}
