﻿using System;
using System.Collections.Generic;
using System.Text;
using static Angel.ShuffleListExtension;
using static Angel.Fish;
using System.Linq;

namespace Angel
{
    class GameEngine
    {
        public static int numbersOfExtraChansOnTrout { get; private set; } = 3;
        public static int numbersOfExtraChansToCatchFish { get; private set; } = 5;
        public static int Score { get; private set; }
        
        private static readonly Random random = new Random();
        private static int bonusPercentOnFish;
        private static int bonusPercentOnTrout;


        private static List<int> luckyHoles;        
        private static int luckyHolePercentBonus = 20;
        private static List<Fish> fishes;

        private static int minChanseForFish = 4;
        private static int maxChanseForFish = 8;
        
        public static void StartNewGame()
        {
            luckyHoles = new List<int>();
            fishes = new List<Fish>();

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
        public static bool ExtraChanseOnTrout()
        {
            numbersOfExtraChansOnTrout--;
            if (numbersOfExtraChansOnTrout != 0)
            {
                bonusPercentOnTrout = 33;
                return true;
            }
            return false;
        }
        public static bool ExtraChanseToCatchFish()
        {
            numbersOfExtraChansToCatchFish--;
            if (numbersOfExtraChansToCatchFish != 0)
            {
                bonusPercentOnFish = 10;
                return true;
            }
            return false;
        }
                
        public static void CatchFish(List<Hook> hooks)
        {
            hooks.Shuffle();
            //Kolla turhål
            foreach (Hook hook in hooks)
            {
                if (luckyHoles.Contains(hook.PositionOnIce))
                {
                    hook.PlacedOnLucyHole = true;
                }
            }
            //Slumpar antal krokar med chans
            int hooksWithChanse = random.Next(minChanseForFish, maxChanseForFish + 1);
            if (hooks.Count < hooksWithChanse)
            {
                hooksWithChanse = hooks.Count;
            }
            //Slumpar fram fiskar
            //List<Hook> tempHooks = new List<Hook>();
            for (int i = 0; i < hooksWithChanse; i++)
            {
                int hit = random.Next(1, 101);
                hit += bonusPercentOnFish;
                if (hooks[i].PlacedOnLucyHole)
                {
                    hit += luckyHolePercentBonus;
                }
                if (hit > 80)
                {
                    Fish fish = new Fish(bonusPercentOnTrout);
                    fishes.Add(fish);
                    hooks[i].Fish = fish;
                    //tempHooks.Add(hooks[i]);
                }
            }

            ClearBonus();
            //return tempHooks;
        }

        private static void ClearBonus()
        {
            bonusPercentOnFish = 0;
            bonusPercentOnTrout = 0;
        }
        public static List<Fish> GetBasketOfFish()
        {
            return fishes;
        }











    }
}
