using System;
using System.Collections.Generic;
using System.Text;

namespace Angel
{
    public class Player
    {
        public int Id { get; set; }
        public string Nickname { get; private set; }
        public int Score { get; set; }

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
    }
}
