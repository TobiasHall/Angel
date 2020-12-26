﻿using System;
using System.Collections.Generic;
using System.Text;
using static Angel.ShuffleExtension;
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

        private static int minChanseForFish = 3;
        private static int maxChanseForFish = 6;
        
        public static void StartNewGame()
        {
            luckyHoles = new List<int>();
            fishes = new List<Fish>();

            numbersOfExtraChansOnTrout = 3;
            numbersOfExtraChansToCatchFish = 5;
            Score = 0;

            for (int i = 0; i < 5; i++)
            {
                luckyHoles.Add(UniqueRandomInt(1, 40));
            }            
        }
        /// <summary>
        /// Randomizes and returns a unique int value
        /// </summary>
        /// <param name="includedMin"></param>
        /// <param name="includedMax"></param>
        /// <returns></returns>
        private static int UniqueRandomInt(int includedMin, int includedMax)
        {
            int myNumber;
            do
            {
                myNumber = random.Next(includedMin, (includedMax + 1));
            }
            while (luckyHoles.Contains(myNumber));
            return myNumber;
        }
        public static bool ExtraChanseOnTrout()
        {
            numbersOfExtraChansOnTrout--;
            if (numbersOfExtraChansOnTrout != 0)
            {
                bonusPercentOnTrout += 33;
                return true;
            }
            return false;
        }
        public static bool ExtraChanseToCatchFish()
        {
            numbersOfExtraChansToCatchFish--;
            if (numbersOfExtraChansToCatchFish != 0)
            {
                bonusPercentOnFish += 20;
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
            for (int i = 0; i < hooksWithChanse; i++)
            {
                int hit = random.Next(1, 101);
                hit += bonusPercentOnFish;                
                if (hooks[i].PlacedOnLucyHole)
                {
                    hit += luckyHolePercentBonus;
                }                
                if (hit > 85)
                {
                    Fish fish = new Fish(bonusPercentOnTrout);
                    //Behövs nog inte. Ev om jag vill att den ska kika på hur många fiskar som slet sig
                    fishes.Add(fish);
                    hooks[i].Fish = fish;                
                    //AddtoScore(fish);
                }
                else if (hit > 75 && hit <=85)
                {
                    hooks[i].HasWorm = false;
                }
            }
            ClearBonus();            
        }

        public static void AddToScore(Fish fish)
        {
            switch (fish.FishId)
            {
                case (int)FishEnum.Dace:
                    Score += (int)(fish.Weight * 0.57);
                    break;
                case (int)FishEnum.Pike:
                    Score += (int)(fish.Weight * 0.57);
                    break;
                case (int)FishEnum.Perch:
                    Score += (int)(fish.Weight * 1.07);
                    break;
                case (int)FishEnum.Char:
                    Score += (int)(fish.Weight * 1.57);
                    break;
                case (int)FishEnum.Trout:
                    Score += (int)(fish.Weight * 2.07);
                    break;
                default: 
                    throw new ArgumentOutOfRangeException();
            }
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
