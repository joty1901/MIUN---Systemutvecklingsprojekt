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

        public IEnumerable<Score> Highscorelist { get; set; }

        private string Visibility;

        public string _visibility
        {
            get { return Visibility; }
            set { 
                Visibility = value;
                OnPropertyChanged(Visibility);
            }
        }


        public HighscoreViewModel()
        {
            SetTopTenHighscoreToList();
            Back = new RelayCommand(GetBack);
            ViewTopFrequentPlayers = new RelayCommand(SetTopFrequentPlayerToList);
            ViewTopHighscore = new RelayCommand(SetTopTenHighscoreToList);
        }

        public void SetTopTenHighscoreToList()
        {
            Visibility = "Visible";
            Highscorelist = Repository.GetTopTenHigscore();
        }

        public void SetTopFrequentPlayerToList()
        {
            Visibility = "Hidden";
            Highscorelist = Repository.GetTopFrequentPlayers();
        }
    }
}
