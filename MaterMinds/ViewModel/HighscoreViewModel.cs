using MaterMinds.Model;
using MaterMinds.View;
using MaterMinds.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MaterMinds
{
    public class HighscoreViewModel : BaseViewModel
    {

        public List<Highscore> Highscorelist { get; set; }

        public HighscoreViewModel()
        {
            GetHighscores();
            Back = new RelayCommand(GetBack);
        }

        public void GetHighscores()
        {
            Highscorelist = Repository.GetTopTenHigscore();
        }


    }
}
