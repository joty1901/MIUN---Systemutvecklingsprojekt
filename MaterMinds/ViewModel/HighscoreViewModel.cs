using System.Collections.Generic;

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

        private void SetTopTenHighscoreToList(object parameter)
        {
            Highscorelist = Repository.GetTopTenHigscore();
            ViewLabelProperty = "Highscore";
        }

        private void SetTopFrequentPlayerToList(object parameter)
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

        private bool CheckIfCanUse(object parameter)
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
