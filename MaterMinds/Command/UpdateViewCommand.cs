using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaterMinds.Command
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Player")
            {
                viewModel.SelectedViewModel = new SelectPlayerViewModel();
            }
            else if (parameter.ToString() == "Highscore")
            {
                viewModel.SelectedViewModel = new HighscoreViewModel();
            }
        }
    }
}
