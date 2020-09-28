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
        public string ViewLabelProperty { get; set; }
        public ObservableCollection<string> SelectedButtonColor { get; set; } = new ObservableCollection<string> { "Black", "Black" };

        public HighscoreViewModel()
        {
            SetTopTenHighscoreToList();
            MainMenuCommand = new RelayCommand(GetMainMenuView);
            ViewTopFrequentPlayersCommand = new RelayCommand(SetTopFrequentPlayerToList);
            ViewTopHighscoreCommand = new RelayCommand(SetTopTenHighscoreToList);
        }

        public void SetTopTenHighscoreToList()
        {
            Highscorelist = Repository.GetTopTenHigscore();
            ViewLabelProperty = "Highscore";
        }

        public void SetTopFrequentPlayerToList()
        {
            Highscorelist = Repository.GetTopFrequentPlayers();
            ViewLabelProperty = "Frequent Players";
        }
    }
}
