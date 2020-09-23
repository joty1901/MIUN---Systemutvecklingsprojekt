using MaterMinds.View;
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
        public ICommand NewPlayerCommand { get; set; }
        public ICommand ChoosePlayerCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public string Nickname { get; set; }
        public string SearchNickname { get; set; }
        public ObservableCollection<Player> PlayerList { get; set; }
        public Player SelectedPlayer { get; set; }

        public ChoosePlayerViewModel()
        {
            SearchCommand = new RelayCommand(SearchPlayer);
            NewPlayerCommand = new RelayCommand(CreatePlayer);
            ChoosePlayerCommand = new RelayCommand(NewGame);
            MainMenuCommand = new RelayCommand(GetMainMenuView);
            GetPlayers();
        }

        private void CreatePlayer()
        {
            try
            {
                int id = Repository.AddPlayer(Nickname);
                Player player = new Player(id, Nickname);
                //PlayerList.Add(player);
                GetPlayers();
                HighlightSelectedPlayer();
            }
            catch (PostgresException exm)
            {
                var code = exm.SqlState;
                MessageBox.Show($"Nickname {Nickname} already in use!");
            }
        }

        private void HighlightSelectedPlayer()
        {
            if (Nickname == null)
            {
                SelectedPlayer = PlayerList[0];
            }
            else
            {
                for (int i = 0; i < PlayerList.Count; i++)
                {
                    if (PlayerList[i].Nickname.ToLower() == Nickname.ToLower())
                    {
                        SelectedPlayer = PlayerList[i];
                    }
                }
            }
        }

        private void GetPlayers()
        {
            PlayerList = (ObservableCollection<Player>)Repository.GetDbPlayers();
        }

        private void NewGame()
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
        private void SearchPlayer()
        {
            PlayerList.Clear();
            if (SearchNickname == "")
            {
                GetPlayers();
            }
            else
            {
                foreach (Player c in Repository.GetDbPlayers().ToList())
                {
                    if (c.Nickname.ToLower() == SearchNickname.ToLower())
                    {
                        PlayerList.Add(c);
                    }
                }
            }
            HighlightSelectedPlayer();
            
        }

    }
}
