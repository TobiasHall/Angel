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
        private static int score;
        private static readonly Random random = new Random();
        private static int numbersOfExtraChansOnTrout;
        private static int numbersOfExtraChansToCatchFish;


        private static List<int> luckyHoles;
        private static int numberOfHooks = 8;
        private static int luckyHolePercentBonus = 50;
        private static List<Fish> fishes = new List<Fish>();
        private static List<Fish> tempFishes = new List<Fish>();

        private static int minChanseForFish = 4;
        private static int maxChanseForFish = 8;


        private static Dictionary<int, int> positionOfHook222 = new Dictionary<int, int>();
        private static Dictionary<int, int> returnDictionary;
        private static List<int> hookIdHasLucyHoleBonus = new List<int>();
        private static List<int> hitPercentage = new List<int>();
        private static List<int> activeHoleInShuffleList = new List<int>();
        private static int troutBonus = 0;
        
        public static void StartNewGame()
        {
            numbersOfExtraChansOnTrout = 3;
            numbersOfExtraChansToCatchFish = 5;
            luckyHoles = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                luckyHoles.Add(UniqueRandomInt(1, 41));
            }            
        }
        public static bool ExtraChanseOnTrout()
        {
            numbersOfExtraChansOnTrout--;
            if (numbersOfExtraChansOnTrout != 0)
            {
                return true;
            }
            return false;
        }
        public static int ExtraChanseOnTroutChansesLeft()
        {
            return numbersOfExtraChansOnTrout;
        }
        public static bool ExtraChanseToCatchFish()
        {
            numbersOfExtraChansToCatchFish--;
            if (numbersOfExtraChansToCatchFish != 0)
            {
                return true;
            }
            return false;
        }
        public static int ExtraChanseToCatchFishLeft()
        {
            return numbersOfExtraChansToCatchFish;
        }
        public static int GetScore()
        {
            return score;
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
            tempFishes = new List<Fish>();
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
            return random.Next(minChanseForFish, maxChanseForFish+1);
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
                    tempFishes.Add(fish);
                    returnDictionary.Add(positionOfHook222.ElementAt(i).Key, positionOfHook222.ElementAt(i).Value);
                }
            }

        }
        public static List<Fish> GetBasketOfFish()
        {
            return fishes;
        }
        public static List<Fish> GetFishFromLastRound()
        {
            return tempFishes;
        }











    }
}
