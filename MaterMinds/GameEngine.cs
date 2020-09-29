using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MaterMinds
{
    public class GameEngine
    {
        Random random = new Random();
        private Dictionary<int, int> CorrectAnswer { get; set; } = new Dictionary<int, int>();
        private Brush[] HintToAnswer { get; set; }
        private int[] CheckForDoubles { get; set; }
        private int[] SortedAnswerArray { get; set; }

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
        private void SortAnswerArray(Dictionary<int, int> playerGuess)
        {
            for (int i = 0; i < playerGuess.Count; i++)
            {
                switch (playerGuess.ElementAt(i).Key)
                {
                    case 1:
                        SortedAnswerArray[0] = playerGuess.ElementAt(i).Value;
                        break;
                    case 2:
                        SortedAnswerArray[1] = playerGuess.ElementAt(i).Value;
                        break;
                    case 3:
                        SortedAnswerArray[2] = playerGuess.ElementAt(i).Value;
                        break;
                    case 4:
                        SortedAnswerArray[3] = playerGuess.ElementAt(i).Value;
                        break;
                }
            }
        }
        private void SortHintArray()
        {
            Array.Sort(HintToAnswer);
            Array.Reverse(HintToAnswer);
        }
        private void GetAnswerArray()
        {
            for (int i = 0; i < 4; i++)
            {
                CheckForDoubles[i] = CorrectAnswer.ElementAt(i).Value;
            }
        }
        private void SetWhitePegs()
        {
            for (int i = 0; i < SortedAnswerArray.Length; i++)
            {
                for (int j = 0; j < CorrectAnswer.Count; j++)
                {
                    if (SortedAnswerArray[i] == CheckForDoubles[j] )
                    {
                        HintToAnswer[i] = Brushes.White;
                        //Set the value to 10 so it never hits again. 
                        CheckForDoubles[j] = 10;
                        break;
                    }
                }
            }
        }
        private void SetBlackPegs()
        {
            int counter = 0;
            for (int i = 0; i < CorrectAnswer.Count; i++)
            {
                if (SortedAnswerArray[i] == CorrectAnswer.ElementAt(i).Value)
                {
                    HintToAnswer[counter] = Brushes.Black;
                    counter++;
                }
            }
        }
        private void ClearAllProps()
        {
            HintToAnswer = new Brush[4];
            CheckForDoubles = new int[4];
            SortedAnswerArray = new int[4];

        }
        public Brush[] CheckPegPosition(Dictionary<int, int> playerGuess)
        {
            ClearAllProps();
            GetAnswerArray();
            SortAnswerArray(playerGuess);
            SetWhitePegs();
            SortHintArray();
            SetBlackPegs();
            return HintToAnswer;
        }
        public bool CheckWinCon(Dictionary<int, int> playerGuess)
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
                return true; 
            }
            return false;
        }

        public Dictionary<int, int> GetCorrectAnswer()
        {
            return CorrectAnswer;
        }
        public int CalculateScore(int tries, int timerInSecounds, int timerInMinutes)
        {
            int score = 10000;
            int timer = timerInSecounds + (timerInMinutes * 60);
            score -= (tries * 1489) + timer*3;
            if (score <= 0)
            {
                score = 0;
            }
            return score;
        }
    }
}
