using MaterMinds;
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
        public string ChoosenPage { get; set; }
        public ObservableCollection<string> SelectedButtonColor { get; set; } = new ObservableCollection<string> { "Black", "Black" };
        public string Visibility { get; set; }

        public HighscoreViewModel()
        {
            SetTopTenHighscoreToList();
            MainMenuCommand = new RelayCommand(GetMainMenuView);
            ViewTopFrequentPlayers = new RelayCommand(SetTopFrequentPlayerToList);
            ViewTopHighscore = new RelayCommand(SetTopTenHighscoreToList);
        }

        public void SetTopTenHighscoreToList()
        {
            Highscorelist = Repository.GetTopTenHigscore();
            ChoosenPage = "Highscore";
            SelectedButtonColor[0] = "Gray";
            SelectedButtonColor[1] = "Black";
            Visibility = "Visible";
        }

        public void SetTopFrequentPlayerToList()
        {
            Highscorelist = Repository.GetTopFrequentPlayers();
            ChoosenPage = "Frequent Players";
            SelectedButtonColor[0] = "Black";
            SelectedButtonColor[1] = "Gray";
            Visibility = "Hidden";
        }
    }
}
