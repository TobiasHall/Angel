using System;
using System.Collections.Generic;
using System.Text;

namespace Angel
{
    public class MockData
    {
        public static Player MockPlayer { get; set; }
        public static int MockGameTime { get; set; } = 150;

        private static readonly Random random = new Random();

        public MockData()
        {
            MockPlayer = new Player();
            MockPlayer.SetPlayerNickname(RandomizeName());
            MockPlayer.SetPlayerScore(RandomizeScore());
        }
        private string RandomizeName()
        {
            List<string> names = new List<string>();
            names.Add("MockJanne");
            names.Add("MockKalle");
            names.Add("MockOlle");
            names.Add("MockNils");
            names.Add("MockUlf");
            names.Add("MockKlas");
            names.Add("MockTorsten");
            names.Add("MockUlla");
            names.Add("MockElla");
            names.Add("MockClara");
            names.Add("MockJenny");
            names.Add("MockWilliam");
            names.Add("MockTure");
            names.Add("MockStina");
            names.Add("MockTuva");
            names.Add("MockLars");
            names.Add("MockStina");
            names.Add("MockEbba");
            names.Add("MockLina");
            names.Add("MockBerit");            
            return names[random.Next(0, names.Count)];
        }
        private int RandomizeScore()
        {
            return random.Next(1000, 2000);
        }
    }
}
