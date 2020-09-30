namespace MaterMinds
{
    interface IRepository
    {

        public  void AddPlayerWithScore(int playerId, int score);

        public  void GetUserHighscore(Player player);

        public  void GetTopTenHigscore();

        public  void AddPlayer(string nickname);

    }
}

