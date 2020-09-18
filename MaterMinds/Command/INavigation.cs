using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaterMinds.Command
{
    public interface INavigation
    {
        public ICommand Back { get; set; }
        public ICommand ViewTopHighscore { get; set; }
        public ICommand ViewTopFrequentPlayers { get; set; }

    }
}
