using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds.Model
{
    class Key : IKey
    {
        public int[] GetKey()
        {
            return null;
        }
    }

    public class MockKey : IKey
    {
        public int[] GetKey()
        {
            return new int[] { 1, 1, 1 };
        }
    }
}
