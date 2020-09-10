using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MaterMinds
{
    public class ChoosePlayerViewModel : BaseViewModel, IRepository
    {
        public ICommand NewPlayer { get; set; }
        public ICommand ChoosePlayer { get; set; }
        public string Nickname { get; set; }
        public List<Player> PlayerList { get; set; }

        public ChoosePlayerViewModel()
        {
            NewPlayer = new RelayCommand(CreatePlayer);
            GetPlayers();
        }
        public void CreatePlayer()
        {
            IRepository.AddPlayer(Nickname);
        }

        public void GetPlayers()
        {
            PlayerList = IRepository.GetDbPlayers().ToList();
        }

    }
}
