using System;
using System.Collections.Generic;
using System.Text;

namespace Angel
{
    public class Player
    {
        public string Nickname { get; set; }
        public int Score { get; private set; }
        public List<Fish> FishBucket { get; set; }

        public Player()
        {

        }
        public string SetPlayerNickname(string nickname)
        {
            if (nickname == null)
            {
                Nickname = "Fräddrik";
                return Nickname;
            }
            else
            {
                Nickname = nickname;
                return Nickname;
            }
        }
        public void SetPlayerScore(int score)
        {
            Score = score;
        }
    }
}
