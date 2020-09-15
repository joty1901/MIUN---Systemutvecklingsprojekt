using MaterMinds.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public ChoosePlayerViewModel()
        {
            NewPlayer = new RelayCommand(CreatePlayer);
            ChoosePlayer = new RelayCommand(NewGame);
            Back = new RelayCommand(GetBack);
            GetPlayers();
        }
        public void CreatePlayer()
        {
            Repository.AddPlayer(Nickname);
            Main.Content = new GamePage();
        }

        public void GetPlayers()
        {
            PlayerList = Repository.GetDbPlayers().ToList();
        }
        public void NewGame()
        {
            Main.Content = new GamePage();
        }

    }
}
