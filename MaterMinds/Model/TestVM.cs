using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds.Model
{
    class TestVM
    {
        IKey key;

        public TestVM(IKey key)
        {
            key = new Key();
        }
    }
}
