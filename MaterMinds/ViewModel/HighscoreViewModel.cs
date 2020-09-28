﻿using MaterMinds;
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

        public HighscoreViewModel()
        {
            SetTopTenHighscoreToList(true);
            MainMenuCommand = new RelayCommand(GetMainMenuView, AlwaysTrue);
            ViewTopFrequentPlayersCommand = new RelayCommand(SetTopFrequentPlayerToList, AlwaysTrue);
            ViewTopHighscoreCommand = new RelayCommand(SetTopTenHighscoreToList, AlwaysTrue);
        }

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
    }
}
