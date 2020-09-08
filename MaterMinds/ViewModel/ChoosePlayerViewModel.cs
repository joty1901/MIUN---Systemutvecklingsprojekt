using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaterMinds
{
    public class ChoosePlayerViewModel : BaseViewModel
    {
        public ICommand Player { get; set; }
        public string Nickname { get; set; }

        public ChoosePlayerViewModel()
        {
            Player = new RelayCommand(CreatePlayer);
        }
        public void CreatePlayer()
        {
            Player player = new Player(Nickname);
            Repository.AddPlayer(player);
        }

    }
}
