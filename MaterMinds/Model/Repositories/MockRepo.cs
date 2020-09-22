using MaterMinds.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;

namespace MaterMinds.Model.Repositories
{
    class MockRepo : IRepository
    {
        private List<Player> players = new List<Player>();

        public void AddPlayer(string nickname)
        {
            Player player = new Player { Nickname = nickname };
            players.Add(player);
        }

        public void AddPlayerWithScore(int playerId, int score)
        {
            throw new NotImplementedException();
        }

        public void GetTopTenHigscore()
        {
            throw new NotImplementedException();
        }

        public void GetUserHighscore(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
