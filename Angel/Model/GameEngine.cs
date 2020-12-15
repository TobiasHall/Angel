using System;
using System.Collections.Generic;
using System.Text;
using static Angel.ShuffleListExtension;
using static Angel.Fish;
using System.Linq;

namespace Angel
{
    class GameEngine
    {
        private static readonly Random random = new Random();
        private static List<int> luckyHoles;
        private static int numberOfHooks = 8;
        private static int luckyHolePercentBonus = 50;
        private static List<Fish> fishes = new List<Fish>();


        private static Dictionary<int, int> positionOfHook222 = new Dictionary<int, int>();
        private static Dictionary<int, int> returnDictionary;
        private static List<int> hookIdHasLucyHoleBonus = new List<int>();
        private static List<int> hitPercentage = new List<int>();
        private static List<int> activeHoleInShuffleList = new List<int>();
        private static int troutBonus = 0;


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

        public static Dictionary<int, int> CatchFish(Dictionary<int, int> positionOfHook)
        {
            ClearProps();
            

            positionOfHook222 = positionOfHook.Shuffle();
            CheckLuckyHoleBonus(positionOfHook);
            SetHitPercentaget();
            GetShuffledActiveHoles(positionOfHook);
            GoFish();



            return returnDictionary;
        }

        private static void ClearProps()
        {
            hookIdHasLucyHoleBonus = new List<int>();
            hitPercentage = new List<int>();
            activeHoleInShuffleList = new List<int>();
            troutBonus = 0;
            returnDictionary = new Dictionary<int, int>();
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
        private static void GetShuffledActiveHoles(Dictionary<int, int> positionOfHook)
        {
            foreach (var item in positionOfHook)
            {
                activeHoleInShuffleList.Add(item.Value);
            }
            activeHoleInShuffleList.Shuffle();            
        }

        

        private static void SetHitPercentaget()
        {
            int number = HooksWithChanceOfFish();
            for (int i = 0; i < number; i++)
            {
                hitPercentage.Add(random.Next(1, 101));
            }

        }
        private static int HooksWithChanceOfFish()
        {
            return random.Next(4, 9);
        }
        private static void GoFish()
        {
            for (int i = 0; i < hitPercentage.Count; i++)
            {
                if (hookIdHasLucyHoleBonus.Contains(positionOfHook222.ElementAt(i).Value))
                {
                    hitPercentage[i] += luckyHolePercentBonus;
                }
                if (hitPercentage[i] > 80)
                {
                    Fish fish = new Fish(troutBonus);
                    fishes.Add(fish);
                    returnDictionary.Add(positionOfHook222.ElementAt(i).Key, positionOfHook222.ElementAt(i).Value);
                }
            }

        }
    }
}
