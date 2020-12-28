using System;
using System.Collections.Generic;
using System.Text;

namespace Angel
{
    public class EndViewModel : BaseViewModel
    {
        public int PlayerScore { get; set; }

        private Player player;


        public EndViewModel(Player player)
        {
            this.player = player;
            PlayerScore = player.Score;
        }
    }
}
