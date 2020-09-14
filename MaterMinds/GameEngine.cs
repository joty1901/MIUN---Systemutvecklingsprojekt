using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MaterMinds
{
    public class GameEngine
    {
        //GameViewModel gameViewModel;
        Random random = new Random();
        private Dictionary<int, int> CorrectAnswer { get; set; } = new Dictionary<int, int>();
        public bool WinCondition { get; private set; }

        public GameEngine()
        {
            StartGame();
        }

        public void StartGame()
        {
            for (int i = 1; i <= 4; i++)
            {
                CorrectAnswer.Add(i, random.Next(1, 7));
            }
        }
        public string[] CheckPegPosition(Dictionary<int, int> playerGuess)
        {
            string[] hintToAnswer = new string[4];
            int[] checkForDoubles = new int[4];
            for (int i = 0; i < 4; i++)
            {
                checkForDoubles[i] = CorrectAnswer.ElementAt(i).Value;
            }
            int counter = 0;
            for (int i = 0; i < playerGuess.Count; i++)
            {
                for (int j = 0; j < playerGuess.Count; j++)
                {
                    if (playerGuess.ElementAt(i).Value == checkForDoubles[j] && counter == 0)
                    {
                        hintToAnswer[i] = "White";
                        checkForDoubles[j] = 0;
                        counter++;
                    }
                }
                counter = 0;
            }
            for (int i = 0; i < playerGuess.Count; i++)
            {
                if (playerGuess.ElementAt(i).Value == CorrectAnswer.ElementAt(i).Value 
                    && playerGuess.ElementAt(i).Key == CorrectAnswer.ElementAt(i).Key)
                {
                    hintToAnswer[counter] = "Black";
                    counter++;
                }
            }
            return hintToAnswer;
        }
        public void CheckWinCon(Dictionary<int, int> playerGuess)
        {
            int counter = 0; 
            foreach (var c in playerGuess)
            {
                foreach (var b in CorrectAnswer)
                {
                    if (c.Key == b.Key && c.Value == b.Value)
                    {
                        counter++; 
                    }
                }
            }
            if (counter == CorrectAnswer.Count)
            {
                WinCondition = true; 
            }
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
        public Dictionary<int, int> GetCorrectAnswer()
        {
            return CorrectAnswer;
        }
    }
}
