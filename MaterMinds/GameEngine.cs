using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Threading;
namespace MaterMinds
{
    public class GameEngine
    {
        Random random = new Random();
        public List<int> correctColor { get; set; }

        public List<int> hintArray { get; set; }

        int arrayLenght = 4;
        List<int> answer = new List<int>() { 4, 2, 3, 2 };
        public GameEngine()
        {
            correctColor = new List<int>() { 0, 0, 0, 0 };
            hintArray = new List<int>() { 0, 0, 0, 0 };
            StartGame();
            CheckPegPosition(answer);
        }

        public void StartGame()
        {

            for (int i = 0; i < 4; i++)
            {
                correctColor[i] = random.Next(1, 5);
            }

        }
        public void CheckPegPosition(List<int> answer)
        {
            List<int> checkList = new List<int>() { 0, 0, 0, 0 };
            for (int i = 0; i < answer.Count; i++)
            {
                if (correctColor.Contains(answer[i]) && !checkList.Contains(answer[i]))
                {
                    hintArray[i] = 1;
                    checkList.Add(answer[i]);
                }
            }
            for (int i = 0; i < answer.Count; i++)
            {
                if (answer[i] == correctColor[i])
                {
                    hintArray[i] = 2;
                }
            }
            hintArray.Sort();
            hintArray.Reverse();

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
