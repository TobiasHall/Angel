using System;
using System.Collections.Generic;
using System.Text;

namespace Angel
{
    [Serializable]
    public class Fish
    {
        public int FishId { get; set; }
        public string Species { get; set; }
        public int Weight { get; set; }
        public Uri imagePathFish { get; set; }

        private static readonly Random random = new Random();        

        public Fish(int troutBonus)
        {
            CreateFish(troutBonus);            
        }
        private void CreateFish(int troutBonus)
        {
            int randomNumber = random.Next(1, 101);
            randomNumber += troutBonus;
            if (randomNumber < 31)
            {
                FishId = (int)FishEnum.Dace;
                Species = "Mört";                
                Weight = RandomizeWeight(FishId);
                imagePathFish = new Uri(@"/Resources/Images/Fishes/dace.png", UriKind.Relative);
            }
            else if (randomNumber >= 31 && randomNumber < 41)
            {
                FishId = (int)FishEnum.Pike;
                Species = "Gädda";
                Weight = RandomizeWeight(FishId);
                imagePathFish = new Uri(@"/Resources/Images/Fishes/pike.png", UriKind.Relative);
            }
            else if (randomNumber >= 41 && randomNumber < 71)
            {
                FishId = (int)FishEnum.Perch;
                Species = "Abbore";
                Weight = RandomizeWeight(FishId);
                imagePathFish = new Uri(@"/Resources/Images/Fishes/perch.png", UriKind.Relative);
            }
            else if (randomNumber >= 71 && randomNumber < 91)
            {
                FishId = (int)FishEnum.Char;
                Species = "Röding";
                Weight = RandomizeWeight(FishId);
                imagePathFish = new Uri(@"/Resources/Images/Fishes/char.png", UriKind.Relative);
            }
            else
            {
                FishId = (int)FishEnum.Trout;
                Species = "Öring";
                Weight = RandomizeWeight(FishId);
                imagePathFish = new Uri(@"/Resources/Images/Fishes/trout.png", UriKind.Relative);
            }
        }
        private int RandomizeWeight(int fishId)
        {
            int weightReducer = random.Next(1, 4);
            int weight;
            switch (fishId)
            {
                case (int)FishEnum.Dace:
                    weight = random.Next(300, 701);
                    break;
                case (int)FishEnum.Pike:
                    weight = random.Next(1000, 3001);
                    break;
                case (int)FishEnum.Perch:
                    weight = random.Next(300, 801);
                    break;
                case (int)FishEnum.Char:
                    weight = random.Next(500, 1001);
                    break;
                case (int)FishEnum.Trout:
                    weight = random.Next(900, 2000);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (weightReducer < 3)
            {
                weight = (int)(weight * 0.7);
            }
            return weight;
        }
        public override string ToString()
        {
            return $"{Species} {Weight}g";
        }
    }
}
