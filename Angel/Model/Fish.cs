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

        private static readonly Random random = new Random();

        public Fish(int troutBonus)
        {
            CreateNewFish(troutBonus);
        }
        private void CreateNewFish(int troutBonus)
        {
            FishId = RandomizeSpecies(troutBonus);
            Species = SetFishSpecies(FishId);
            Weight = SetWeightOfFish(FishId);
        }
        private int RandomizeSpecies(int troutBonus)
        {
            int randomNumber = random.Next(1, 101);
            randomNumber += troutBonus;
            if (randomNumber < 31)
            {
                return (int)FishEnum.Dace;
            }
            else if (randomNumber >= 31 && randomNumber <41)
            {
                return (int)FishEnum.Pike;
            }
            else if (randomNumber >= 41 && randomNumber < 71)
            {
                return (int)FishEnum.Perch;
            }
            else if (randomNumber >= 71 && randomNumber < 91)
            {
                return (int)FishEnum.Char;
            }
            else
            {
                return (int)FishEnum.Trout;
            }            
        }
        private string SetFishSpecies(int fishId)
        {
            switch (fishId)
            {
                case (int)FishEnum.Dace:
                    return "Mört";                    
                case (int)FishEnum.Pike:
                    return "Gädda";                    
                case (int)FishEnum.Perch:
                    return "Abbore";                    
                case (int)FishEnum.Char:
                    return "Röding";
                case (int)FishEnum.Trout:
                    return "Öring";
                default: throw new ArgumentOutOfRangeException();
            }
        }
        private int SetWeightOfFish(int fishId)
        {
            switch (fishId)
            {
                case (int)FishEnum.Dace:
                    return random.Next(300, 701);
                case (int)FishEnum.Pike:
                    return random.Next(1000, 3001);
                case (int)FishEnum.Perch:
                    return random.Next(300, 801);
                case (int)FishEnum.Char:
                    return random.Next(500, 1001);
                case (int)FishEnum.Trout:
                    return random.Next(900, 2201);
                default: throw new ArgumentOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return $"{Species} {Weight}g";
        }
    }
}
