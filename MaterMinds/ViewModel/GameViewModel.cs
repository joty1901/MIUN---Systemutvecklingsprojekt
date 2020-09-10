using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace MaterMinds
{
    class GameViewModel : BaseViewModel
    {
        public Dictionary<int, int> PlacedPegs { get; set; } = new Dictionary<int, int>();
        //public PegPosition Peg { get; set; } = PegPosition.NewValue;
        public ObservableCollection<int> MyProperty { get; set; }
        public ObservableCollection<bool> IsCool { get; set; } = new ObservableCollection<bool> { true, false, false, false, false, false };

        public ICommand Någonting { get; set; }
        public int Counter { get; set; } = 1;

        public GameViewModel()
        {

            Någonting = new RelayCommand(CheckBool);
        }


        public void CheckBool()
        {
            if (Counter <= 6)
            {
                IsCool = new ObservableCollection<bool> { false, false, false, false, false, false };
                IsCool[Counter] = true;
                Counter++;
            }


        }
    }
}
