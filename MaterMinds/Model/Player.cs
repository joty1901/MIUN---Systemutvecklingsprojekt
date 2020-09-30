﻿using System.Collections.Generic;

namespace MaterMinds
{
    public class Player
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public List<Score> PlayerScore { get; set; }

        public Player() { }

        public Player(int id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
    }
}
