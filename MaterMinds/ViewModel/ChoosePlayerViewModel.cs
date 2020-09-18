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
        public ICommand NewPlayer { get; set; }
        public ICommand ChoosePlayer { get; set; }
        public string Nickname { get; set; }
        public List<Player> PlayerList { get; set; }
        public Player SelectedPlayer { get; set; }

        public ChoosePlayerViewModel()
        {
            NewPlayer = new RelayCommand(CreatePlayer);
            ChoosePlayer = new RelayCommand(NewGame);
            Back = new RelayCommand(GetBack);
            GetPlayers();
        }

        public void CreatePlayer()
        {
            try
            {
                int id = Repository.AddPlayer(Nickname);
                Player player = new Player(id, Nickname);
                GetPlayers();
            }
            catch (PostgresException exm)
            {
                var code = exm.SqlState;
                MessageBox.Show($"Nickname {Nickname} already in use!");
            }
                for (int i = 0; i < PlayerList.Count; i++)
                {
                    if (PlayerList[i].Nickname == Nickname)
                    {
                        SelectedPlayer = PlayerList[i];
                    }
                }
        }

        public void GetPlayers()
        {
            PlayerList = Repository.GetDbPlayers().ToList();
        }

        public void NewGame()
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

    }
}
