﻿using MaterMinds.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds
{
    public class Player : IRepository
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public List<Score> PlayerScore { get; set; }

        //Konstruktor som används när GetDbPlayers körs i Repository.cs
        public Player()
        {

        }
        
        public Player(int id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
    }
       
}
