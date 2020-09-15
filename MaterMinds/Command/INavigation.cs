using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaterMinds.Command
{
    public interface INavigation
    {
        public ICommand Back { get; set; }
    }
}
