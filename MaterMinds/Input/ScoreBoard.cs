using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds.Input
{
    public class ScoreBoard
    {
        public int Score { get; set; }
        public int ScoreId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public ScoreBoard(User user, int score)
        {
            Score = score;
            UserId = user.Id;
        }
    }
}
