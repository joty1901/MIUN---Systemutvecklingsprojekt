using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }

        public User()
        {

        }

        public User(int id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }


        public override string ToString()
        {
            return $"{Id} {Nickname}";
        }
    }
}
