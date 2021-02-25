using System;
using System.Collections.Generic;

namespace Angel
{
    [Serializable]
    public class Player
    {
        public string Nickname { get; private set; }
        public int Score { get; private set; }
        public List<Fish> FishBucket { get; set; }

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
        public override string ToString()
        {
            return $"{Nickname}.....{Score}p";
        }
    }
}
