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
                Weight = random.Next(300, 701);
                imagePathFish = new Uri(@"/Resources/Images/Fishes/dace.png", UriKind.Relative);
            }
            else if (randomNumber >= 31 && randomNumber < 41)
            {
                FishId = (int)FishEnum.Pike;
                Species = "Gädda";
                Weight = random.Next(1000, 3001);
                imagePathFish = new Uri(@"/Resources/Images/Fishes/pike.png", UriKind.Relative);
            }
            else if (randomNumber >= 41 && randomNumber < 71)
            {
                FishId = (int)FishEnum.Perch;
                Species = "Abbore";
                Weight = random.Next(300, 801);
                imagePathFish = new Uri(@"/Resources/Images/Fishes/perch.png", UriKind.Relative);
            }
            else if (randomNumber >= 71 && randomNumber < 91)
            {
                FishId = (int)FishEnum.Char;
                Species = "Röding";
                Weight = random.Next(500, 1001);
                imagePathFish = new Uri(@"/Resources/Images/Fishes/char.png", UriKind.Relative);
            }
            else
            {
                FishId = (int)FishEnum.Trout;
                Species = "Öring";
                Weight = random.Next(900, 2000);
                imagePathFish = new Uri(@"/Resources/Images/Fishes/trout.png", UriKind.Relative);
            }
        }
        public override string ToString()
        {
            return $"{Species} {Weight}g";
        }
    }
}
