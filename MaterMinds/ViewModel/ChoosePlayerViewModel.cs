﻿using Npgsql;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MaterMinds
{
    public class ChoosePlayerViewModel : BaseViewModel
    {
        #region Commands
        public ICommand NewPlayerCommand { get; set; }
        public ICommand ChoosePlayerCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        #endregion
        #region Properties
        public string Nickname { get; set; }
        public string SearchNickname { get; set; }
        public ObservableCollection<Player> PlayerList { get; set; }
        public Player SelectedPlayer { get; set; }
        #endregion

        public ChoosePlayerViewModel()
        {
            SearchCommand = new RelayCommand(SelectSearchedPlayer, CanExecute);
            NewPlayerCommand = new RelayCommand(CreatePlayer, CanExecute);
            ChoosePlayerCommand = new RelayCommand(NewGame, CeckIfCanExecute);
            MainMenuCommand = new RelayCommand(GetMainMenuView, CanExecute);
            GetPlayers();
        }

        private void GetPlayers()
        {
            PlayerList = (ObservableCollection<Player>)Repository.GetDbPlayers();
        }

        private void CreatePlayer(object parameter)
        {
            try
            {
                int playerID = Repository.AddPlayer(Nickname);
                Player player = new Player(playerID, Nickname);
                GetPlayers();
                HighlightSelectedPlayer();
            }
            catch (PostgresException ex)
            {
                var code = ex.SqlState;
                MessageBox.Show($"Nickname {Nickname} already in use!");
            }
        }

        private void NewGame(object parameter)
        {
            Player player = SelectedPlayer;
            Main.Content = new GamePage(player);
        }

        private void HighlightSelectedPlayer()
        {
            if (Nickname == null)
            {
                if (PlayerList.Count == 1)
                {
                    SelectedPlayer = PlayerList[0];
                }
                else
                {
                    SelectedPlayer = null;
                }
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
            Nickname = null;
        }

        #region SearchPlayers
        private void SelectSearchedPlayer(object parameter)
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
            List<Player> listOfPlayers = Repository.GetDbPlayers().ToList();
            for (int i = 0; i < listOfPlayers.Count; i++)
            {
                for (int j = 0; j < SearchNickname.Count(); j++)
                {
                    if (listOfPlayers[i].Nickname.ToLower()[j] == SearchNickname.ToLower()[j])
                    {
                        if (j == SearchNickname.Count()-1)
                        {
                            PlayerList.Add(listOfPlayers[i]);   
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (PlayerList.Count == 0)
            {
                SearchIfContainsNickname(listOfPlayers);
            }
        }

        private void SearchIfContainsNickname(List<Player> listOfPlayers)
        {
            foreach (Player c in listOfPlayers)
            {
                if (c.Nickname.ToLower().Contains(SearchNickname.ToLower()))
                {
                    PlayerList.Add(c);
                }
            }
        }
        #endregion

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
