using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds.Input
{
    public class Score
    {
        public int Value { get; set; }
        public int ScoreId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public Score(User user, int value)
        {
            Value = value;
            UserId = user.Id;
        }
        public Score()
        {

        }
    }
}
