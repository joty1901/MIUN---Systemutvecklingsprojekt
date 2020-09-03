using System;
using System.Collections.Generic;
using System.Text;

namespace MaterMinds
{
    public class GameEngine
    {
        Random random = new Random();
        private int[] correctColor { get; set; }
        public GameEngine()
        {
            StartGame();
        }

        public void StartGame()
        {
            CreateArray();
        }
        public int[] CreateArray()
        {
            for (int i = 0; i < 4; i++)
            {
                correctColor[i] = random.Next(1, 5);
            }
            return correctColor;
        }
    }
}
