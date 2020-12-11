using System;
using System.Collections.Generic;
using System.Text;

namespace Angel
{
    public class Player
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int Score { get; set; }

        public Player()
        {

        }
        public Player(int id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
    }
}
