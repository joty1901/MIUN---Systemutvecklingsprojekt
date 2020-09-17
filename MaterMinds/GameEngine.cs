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
        private int Score { get; set; } = 10000;



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
            int[] sortedAnswerArray = new int[4];
            for (int i = 0; i < playerGuess.Count; i++)
            {
                if (playerGuess.ElementAt(i).Key == 1)
                {
                    sortedAnswerArray[0] = playerGuess.ElementAt(i).Value;
                }
                else if (playerGuess.ElementAt(i).Key == 2)
                {
                    sortedAnswerArray[1] = playerGuess.ElementAt(i).Value;
                }
                else if (playerGuess.ElementAt(i).Key == 3)
                {
                    sortedAnswerArray[2] = playerGuess.ElementAt(i).Value;
                }
                else if (playerGuess.ElementAt(i).Key == 4)
                {
                    sortedAnswerArray[3] = playerGuess.ElementAt(i).Value;
                }
            }
            
            
            for (int i = 0; i < 4; i++)
            {
                checkForDoubles[i] = CorrectAnswer.ElementAt(i).Value;
            }
            int counter = 0;
            for (int i = 0; i < sortedAnswerArray.Length; i++)
            {
                for (int j = 0; j < CorrectAnswer.Count; j++)
                {
                    if (sortedAnswerArray[i] == checkForDoubles[j] && counter == 0)
                    {
                        hintToAnswer[i] = "White";
                        //Set the value to 10 so it never hits again. 
                        checkForDoubles[j] = 10;
                        counter++;
                    }
                }
                counter = 0;
            }
            Array.Sort(hintToAnswer);
            Array.Reverse(hintToAnswer);
            for (int i = 0; i < CorrectAnswer.Count; i++)
            {
                if (sortedAnswerArray[i] == CorrectAnswer.ElementAt(i).Value)
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

        public DateTime GetDateTime()
        {
            DateTime date = DateTime.Now;
            
            return date;
        }
        public Dictionary<int, int> GetCorrectAnswer()
        {
            return CorrectAnswer;
        }
        public int CalcScore(int tries, int timerInSecounds, int timerInMinutes)
        {
            int timer = timerInSecounds + (timerInMinutes * 60 * 5);
            Score -= (tries * 1000) + timer;
            if (Score <= 0)
            {
                Score = 0;
            }
            return Score;
        }
    }
}
