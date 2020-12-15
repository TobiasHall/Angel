using System;
using System.Collections.Generic;
using System.Text;

namespace Angel
{
    public class Fish
    {
        public int FishId { get; set; }
        public string Species { get; set; }
        public double Weight { get; set; }

        private static readonly Random random = new Random();

        public Fish()
        {
            RandomizeFish();
        }
        private void RandomizeFish()
        {
            FishId = random.Next(0, 5);
            Species = SetFishSpecies(FishId);
            Weight = SetWightOfFish(FishId);
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
        private double SetWightOfFish(int fishId)
        {
            switch (fishId)
            {
                case (int)FishEnum.Dace:
                    return random.Next(300, 801);
                case (int)FishEnum.Pike:
                    return random.Next(300, 801);
                case (int)FishEnum.Perch:
                    return random.Next(300, 801);
                case (int)FishEnum.Char:
                    return random.Next(300, 801);
                case (int)FishEnum.Trout:
                    return random.Next(300, 801);
                default: throw new ArgumentOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return $"{Species} {Weight}";
        }
    }
}
