﻿using MaterMinds.View;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MaterMinds
{
    public class ChoosePlayerViewModel : BaseViewModel
    {
        #region Commands
        public ICommand NewPlayerCommand { get; set; }
        public ICommand ChoosePlayerCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        #endregion

        public string Nickname { get; set; }
        public string SearchNickname { get; set; }
        public ObservableCollection<Player> PlayerList { get; set; }
        public Player SelectedPlayer { get; set; }

        public ChoosePlayerViewModel()
        {
            SearchCommand = new RelayCommand(SearchPlayer, CanExecute);
            NewPlayerCommand = new RelayCommand(CreatePlayer, CanExecute);
            ChoosePlayerCommand = new RelayCommand(NewGame, CeckIfCanExecute);
            MainMenuCommand = new RelayCommand(GetMainMenuView, CanExecute);
            GetPlayers();
        }

        private void CreatePlayer(object parameter)
        {
            try
            {
                int id = Repository.AddPlayer(Nickname);
                Player player = new Player(id, Nickname);
                GetPlayers();
                HighlightSelectedPlayer();
            }
            catch (PostgresException ex)
            {
                var code = ex.SqlState;
                MessageBox.Show($"Nickname {Nickname} already in use!");
            }
        }

        private void HighlightSelectedPlayer()
        {
            if (Nickname == null && PlayerList.Count != 0)
            {
                SelectedPlayer = PlayerList[0];
            }
            else
            {
                GetSelectedPlayerToHiglight();
            }
        }

        private void GetSelectedPlayerToHiglight()
        {
            for (int i = 0; i < PlayerList.Count; i++)
            {
                if (PlayerList[i].Nickname.ToLower() == Nickname.ToLower())
                {
                    SelectedPlayer = PlayerList[i];
                }
            }
        }

        private void GetPlayers()
        {
            PlayerList = (ObservableCollection<Player>)Repository.GetDbPlayers();
        }

        private void NewGame(object parameter)
        {
            if (SelectedPlayer != null)
            {
                Player player = SelectedPlayer;
                Main.Content = new GamePage(player);
            }
            else
            {
                MessageBox.Show("Select a player before starting the game");
            }
        }

        private void SearchPlayer(object parameter)
        {
            ClearPlayerList();
            if (SearchNickname == "" || SearchNickname == null)
            {
                GetPlayers();
            }
            else
            {
                ComparePlayerNickname();
            }
            HighlightSelectedPlayer();
        }

        private void ComparePlayerNickname()
        {
            foreach (Player c in Repository.GetDbPlayers().ToList())
            {
                if (c.Nickname.ToLower().Contains(SearchNickname.ToLower()))
                {
                    PlayerList.Add(c);
                }
            }
        }

        private void ClearPlayerList()
        {
            PlayerList.Clear();
        }

        public override bool CeckIfCanExecute(object parameter)
        {
            if (SelectedPlayer != null)
            {
                return true;
            }
            else
                return false;
        }

    }
}
