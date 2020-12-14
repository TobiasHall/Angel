using System;
using System.Collections.Generic;
using System.Text;
using static Angel.ShuffleList;

namespace Angel
{
    class GameEngine
    {
        private static readonly Random random = new Random();
        private static List<int> luckyHoles;
        private static int numberOfHooks = 8;
        private static int luckyHolePercentBonus = 30;



        private static List<int> hookIdHasLucyHoleBonus = new List<int>();
        private static int[] hitPercentage = new int[8];


        public static void StartNewGame()
        {
            luckyHoles = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                luckyHoles.Add(UniqueRandomInt(1, 41));
            }            
        }

        private static int UniqueRandomInt(int min, int max)
        {
            int myNumber;
            do
            {
                myNumber = random.Next(min, max);
            }
            while (luckyHoles.Contains(myNumber));
            return myNumber;
        }        

        public static void CatchFish(Dictionary<int, int> positionOfHook)
        {            
            CheckLuckyHoleBonus(positionOfHook);
            //CheckIfFishOnHook()
            List<int> activeHoleInShuffleList = new List<int>();
            foreach (var item in positionOfHook)
            {
                activeHoleInShuffleList.Add(item.Value);
            }
            activeHoleInShuffleList.Shuffle();

            

        }

        private static void CheckLuckyHoleBonus(Dictionary<int, int> positionOfHook)
        {            
            foreach (var hookPos in positionOfHook)
            {
                if (luckyHoles.Contains(hookPos.Value))
                {
                    hookIdHasLucyHoleBonus.Add(hookPos.Key);
                }
            }
        }

        

        private static void CheckIfFishOnHook()
        {

            for (int i = 0; i < hitPercentage.Length; i++)
            {
                hitPercentage[i] = random.Next(1, 101);
            }

        }
    }
}
