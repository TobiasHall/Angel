using System;
using System.Collections.Generic;
using System.Text;

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

        private Player player;
        private int gameTimer;

        public EndViewModel(Player player, int gameTimer)
        {
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            HighscoreViewCommand = new RelayCommand(GetHighscoreView, CanExecute);
            MainMenuViewCommand = new RelayCommand(GetMainMenuView, CanExecute);
            ExitGameCommand = new RelayCommand(CloseApplication, CanExecute);

            this.player = player;
            this.gameTimer = gameTimer;
            SetLabels();
        }
        private void GetGameView(object parameter)
        {
            Main.Content = new GameView(player, gameTimer);
        }

        private void SetLabels()
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
                TotalWeight += fish.Weight;
            }
        }
    }
}
