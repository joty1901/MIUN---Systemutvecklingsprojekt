using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds
{
    public class Player
    {
        public int Id { get; set; }
        public string Nickname { get; set; }

        public Player(string nickname)
        {
            Nickname = nickname;
        }
        public Player(int id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
    }
}
