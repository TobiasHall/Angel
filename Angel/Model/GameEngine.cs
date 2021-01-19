using System;
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

        private static int minHooksToTrigger = 3;
        private static int maxHooksToTrigger = 6;
        
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
                bonusPercentOnTrout += 20;
                return true;
            }
            return false;
        }
        public static bool ExtraChanseToCatchFish()
        {
            numbersOfExtraChansToCatchFish--;
            if (numbersOfExtraChansToCatchFish != 0)
            {
                bonusPercentOnFish += 10;
                return true;
            }
            return false;
        }
                
        public static List<Hook> CatchFish(List<Hook> hooks)
        {
            List<Hook> tempHooks = new List<Hook>(hooks);
            tempHooks.Shuffle();

            //Kolla turhål
            foreach (Hook hook in tempHooks)
            {
                if (luckyHoles.Contains(hook.PositionOnIce))
                {
                    hook.PlacedOnLucyHole = true;
                }
            }
            //Slumpar antal krokar med chans till fisk
            int hooksWithChanse = random.Next(minHooksToTrigger, maxHooksToTrigger + 1);
            if (tempHooks.Count < hooksWithChanse)
            {
                hooksWithChanse = tempHooks.Count;
            }
            //Slumpar fram om krok får fisk, blir av med mask, eller inget
            for (int i = 0; i < hooksWithChanse; i++)
            {
                int hit = random.Next(1, 101);
                hit += bonusPercentOnFish;                
                if (tempHooks[i].PlacedOnLucyHole)
                {
                    hit += luckyHolePercentBonus;
                }                
                if (hit > 85)
                {
                    tempHooks[i].Fish = new Fish(bonusPercentOnTrout);
                    tempHooks[i].HasWorm = false;                    
                }
                else if (hit > 80 && hit <= 85)
                {
                    tempHooks[i].HasWorm = false;
                }
            }
            ClearBonus();
            return tempHooks;
        }

        public static void AddToScore(Fish fish)
        {
            switch (fish.FishId)
            {
                case (int)FishEnum.Dace:
                    Score += (int)(fish.Weight * 0.87);
                    break;
                case (int)FishEnum.Pike:
                    Score += (int)(fish.Weight * 0.87);
                    break;
                case (int)FishEnum.Perch:
                    Score += (int)(fish.Weight * 1.07);
                    break;
                case (int)FishEnum.Char:
                    Score += (int)(fish.Weight * 1.27);
                    break;
                case (int)FishEnum.Trout:
                    Score += (int)(fish.Weight * 1.47);
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
    }
}
