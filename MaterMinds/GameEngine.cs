using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MaterMinds
{
    public class GameEngine
    {
        //GameViewModel gameViewModel;
        Random random = new Random();
        private Dictionary<int, int> CorrectAnswer { get; set; } = new Dictionary<int, int>();

        public GameEngine()
        {
            
            StartGame();
            //CheckPegPosition(answer);
        }

        public void StartGame()
        {
            for (int i = 1; i <= 4; i++)
            {
                CorrectAnswer.Add(i, random.Next(1, 7));
            }
            

        }
        public int[] CheckPegPosition(Dictionary<int, int> playerGuess)
        {

            int[] hintToAnswer = new int[4];
            List<int> checkList = new List<int>() { 0, 0, 0, 0};
            int counter = 0;
            foreach (int c in playerGuess.Values)
            {
                if (CorrectAnswer.ContainsValue(c) && !checkList.Contains(c))
                {
                    hintToAnswer[counter] = 1;
                    checkList[counter] = c;
                    counter++;
                }
                
            }
            counter = 0; 
            foreach (var c in playerGuess)
            {
                foreach (var b in CorrectAnswer)
                {
                    if (c.Key == b.Key && c.Value == b.Value)
                    {
                        hintToAnswer[counter] = 2;
                        counter++;
                    }
                }
                
            }
            //Array.Sort(hintToAnswer);
            //Array.Reverse(hintToAnswer);
            return hintToAnswer;
        }

        public string CalcTime(DateTime date, DateTime date2 )
        {
            string result = date2.Subtract(date).ToString();

            return result;
        }

        public DateTime GetDateTime()
        {
            DateTime date = DateTime.Now;
            
            return date;
        }
    }
}
