using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds
{
    class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }

        public User(int id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
    }
}
