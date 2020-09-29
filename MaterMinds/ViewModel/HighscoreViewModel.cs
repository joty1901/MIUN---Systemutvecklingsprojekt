using MaterMinds;
using MaterMinds.View;
using MaterMinds.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MaterMinds
{
    public class HighscoreViewModel : BaseViewModel
    {
        public IEnumerable<Score> Highscorelist { get; set; }
        public string ViewLabelProperty { get; set; } = "Highscore";

        public HighscoreViewModel()
        {
            SetTopTenHighscoreToList(true);
            MainMenuCommand = new RelayCommand(GetMainMenuView, CanExecute);
            ViewTopFrequentPlayersCommand = new RelayCommand(SetTopFrequentPlayerToList, CeckIfCanExecute);
            ViewTopHighscoreCommand = new RelayCommand(SetTopTenHighscoreToList, CheckIfCanUse);
        }

        #region GetDifferentScoreLists

        public void SetTopTenHighscoreToList(object parameter)
        {
            Highscorelist = Repository.GetTopTenHigscore();
            ViewLabelProperty = "Highscore";
        }

        public void SetTopFrequentPlayerToList(object parameter)
        {
            Highscorelist = Repository.GetTopFrequentPlayers();
            ViewLabelProperty = "Frequent Players";
        }
        #endregion

        #region CheckIfMethodsCanExecute

        public override bool CeckIfCanExecute(object parameter)
        {
            if (ViewLabelProperty == "Frequent Players")
            {
                return false;
            }
            return true;
        }
        public bool CheckIfCanUse(object parameter)
        {
            if (ViewLabelProperty == "Highscore")
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
