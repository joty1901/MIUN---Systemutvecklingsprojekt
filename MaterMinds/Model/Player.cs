using MaterMinds.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds
{
    public class Player
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public List<Score> PlayerScore { get; set; }

        /// <summary>
        /// Constructor that's used when the method GetDbPlayers i executed in Repository.cs
        /// </summary>
        public Player() 
        {

        }

        /// <summary>
        /// Constructor that's used when a user creates a new player
        /// </summary>
        public Player(int id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
    }
}
