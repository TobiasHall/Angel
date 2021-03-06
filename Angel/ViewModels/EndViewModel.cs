﻿using System;

namespace Angel
{
    public class EndViewModel : BaseViewModel
    {        
        public int NrOfDace { get; set; }
        public int NrOfPike { get; set; }
        public int NrOfPerc { get; set; }
        public int NrOfChar { get; set; }
        public int NrOfTrout { get; set; }
        public int TotalFishes { get; set; }
        public decimal TotalWeight { get; set; } = 0;
        public int TotalScore { get; set; }
        public string HighscorePlacement { get; set; }

        private Player player;
        private TimeSpan gameTimer;
        private Highscore highscore;
        private bool addPlayerToHighscore;

        public EndViewModel(Player player, TimeSpan gameTimer)
        {
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            HighscoreViewCommand = new RelayCommand(GetHighscoreView, CanExecute);
            MainMenuViewCommand = new RelayCommand(GetMainMenuView, CanExecute);
            ExitGameCommand = new RelayCommand(CloseApplication, CanExecute);
            this.player = player;
            this.highscore = new Highscore() {LatestRoundPlayer = player};
            this.gameTimer = gameTimer;
            
            SetLabels();

            CheckForHighscore();
        }

        private void CheckForHighscore()
        {
            addPlayerToHighscore =  highscore.AddtoHighscore(player);
            if (addPlayerToHighscore)
            {
                int index = highscore.GetHighscorePlacement(player);
                HighscorePlacement = index.ToString();
            }
            else
            {
                HighscorePlacement = "-";
            }
        }
        private void GetGameView(object parameter)
        {
            Main.Content = new GameView(player, gameTimer);
        }

        private void SetLabels()
        {
            if (player.FishBucket != null)
            {
                foreach (Fish fish in player.FishBucket)
                {
                    switch (fish.FishId)
                    {
                        case (int)FishEnum.Dace:
                            NrOfDace++;
                            break;
                        case (int)FishEnum.Pike:
                            NrOfPike++;
                            break;
                        case (int)FishEnum.Perch:
                            NrOfPerc++;
                            break;
                        case (int)FishEnum.Char:
                            NrOfChar++;
                            break;
                        case (int)FishEnum.Trout:
                            NrOfTrout++;
                            break;
                        default:
                            break;
                    }
                    TotalFishes++;
                    TotalWeight += Math.Round((decimal)fish.Weight / 1000, 2);
                }
            }            
            TotalScore = player.Score;
        }
    }
}
