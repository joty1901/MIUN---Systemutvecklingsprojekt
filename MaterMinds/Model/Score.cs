using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds
{
    public class Score
    {
        public int Value { get; set; }
        public int ScoreId { get; set; }
        public int PlayerId { get; set; }
        public DateTime Date { get; set; }

        public Score(Player player, int value)
        {
            Value = value;
            PlayerId = player.Id;
            Repository.AddPlayerScore(PlayerId, Value);
        }
        public Score()
        {

        }

    }
}
