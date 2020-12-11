using System;
using System.Collections.Generic;
using System.Text;

namespace Angel
{
    class GameEngine
    {
        private static readonly Random random = new Random();
        private static int[] luckyHoles = new int[5];
        public static void StartNewGame()
        {
            for (int i = 0; i < luckyHoles.Length; i++)
            {
                luckyHoles[i] = random.Next(0, 40);
            }
        }

        public static void CatchFish(Dictionary<int, int> positionOfHook)
        {

        }
    }
}
