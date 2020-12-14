using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using static Angel.GameEngine;

namespace Angel
{
    public class GameViewModel : BaseViewModel
    {
        public Player player { get; set; }
        public string GameTimer { get; set; } = "00:30:00";
        public int NrOfHooks { get; set; } = 0;
        public Uri ImageUri { get; set; } = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
        public Uri[] ImageUri2 { get; set; } = new Uri[8] {new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/fish.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative)};
        public Dictionary<int, int> PositionOfHook { get; set; } = new Dictionary<int, int>();
        //public List<int> HookOnIce { get; set; }
        

        DispatcherTimer timer;
        int countdownTimer = 1800;
        int CatchFishTrigger;


        public GameViewModel(Player player)
        {
            MainMenuCommand = new RelayCommand(GetMainMenuView, CanExecute);
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);

            this.player = player;
            StartNewGame();
            StartCountdown();
        }

        private void StartCountdown()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (countdownTimer != 0)
            {
                countdownTimer--;
                GameTimer = countdownTimer.ToString();
                GameTimer = String.Format("00:{0:D2}:{1:D2}", countdownTimer / 60, countdownTimer % 60);
                CatchFishTrigger++;

                if (CatchFishTrigger == 10)
                {
                    //Test av ImageUri
                    ImageUri = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                    //Trigga metod som slår ut angeldon
                    CatchFish(PositionOfHook);
                }
            }
            else
            {
                timer.Stop();
                //Metod för att sluta spelet
            }

        }

        private void GetGameView(object parameter)
        {            
            Main.Content = new GameView(player);
        }
    }
}
    
