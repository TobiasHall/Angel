using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using static Angel.GameEngine;

namespace Angel
{
    public class GameViewModel : BaseViewModel
    {
        public ICommand LuckySnuffBtn { get; set; }
        public ICommand CupOfCoffeBtn { get; set; }
        public bool LuckySnuffBtnEnabled { get; set; } = true;
        public int LuckySnuffLabel { get; set; }
        public bool CupOfCoffeBtnEnabled { get; set; } = true;
        public int CupOfCoffeLabel { get; set; }
        public int NrOfDace { get; set; }
        public int NrOfPike { get; set; }
        public int NrOfPerc { get; set; }
        public int NrOfChar { get; set; }
        public int NrOfTrout { get; set; }
        public int TotalFishes { get; set; }
        public int TotalWeight { get; set; }
        public int TotalScore { get; set; }


        public Player player { get; set; }
        public string GameTimer { get; set; } = "00:30:00";
        public int NrOfHooks { get; set; } = 0;
        public Uri HookId100 { get; set; } = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
        public Uri HookId101 { get; set; } = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
        public Uri ImageUri { get; set; } = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
        public Uri[] ImageUri2 { get; set; } = new Uri[8] {new Uri("/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative)};
        public Dictionary<int, int> PositionOfHook { get; set; } = new Dictionary<int, int>();
        public Dictionary<Hook, int> PositionOfHook2 { get; set; } = new Dictionary<Hook, int>();
        //public List<int> HookOnIce { get; set; }
        public Dictionary<int, int> HookHasFish { get; set; } = new Dictionary<int, int>();


        DispatcherTimer timer;
        int countdownTimer = 1800;
        int CatchFishTrigger;


        public GameViewModel(Player player)
        {
            MainMenuCommand = new RelayCommand(GetMainMenuView, CanExecute);
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            LuckySnuffBtn = new RelayCommand(UseLuckySnuff, CanExecute);
            CupOfCoffeBtn = new RelayCommand(UseCupOfCoffe, CanExecute);

            this.player = player;
            StartNewGame();
            LuckySnuffLabel = ExtraChanseOnTroutChansesLeft();
            CupOfCoffeLabel = ExtraChanseToCatchFishLeft();


            StartCountdown();
        }
        public void SetImage(object parameter)
        {
            
        }
        private void UseLuckySnuff(object parameter)
        {
            LuckySnuffBtnEnabled = ExtraChanseOnTrout();
            LuckySnuffLabel = ExtraChanseOnTroutChansesLeft();
        }
        private void UseCupOfCoffe(object parameter)
        {
            CupOfCoffeBtnEnabled = ExtraChanseToCatchFish();
            CupOfCoffeLabel = ExtraChanseToCatchFishLeft();
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
                    //Test av att ha Hook i en dictionary
                    foreach (var item in TestAvDict(PositionOfHook2))
                    {
                        Hook hook = item.Key;
                        hook.ImageUri33 = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                    }



                    //Test av ImageUri
                    ImageUri = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                    HookId100 = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                    HookId101 = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);

                    var hooksWithFish = CatchFish(PositionOfHook);


                    var fishToLabel = GetFishFromLastRound();
                    foreach (var fish in fishToLabel)
                    {
                        TotalFishes++;
                        TotalWeight += fish.Weight;
                        if (fish.FishId == (int)FishEnum.Dace)
                        {

                        }
                        else if (fish.FishId == (int)FishEnum.Pike)
                        {

                        }
                        else if (fish.FishId == (int)FishEnum.Perch)
                        {

                        }
                        else if (fish.FishId == (int)FishEnum.Char)
                        {

                        }
                        else
                        {

                        }
                    }
                    TotalScore = GetScore();


                    
                    //UpdateHookImage(hooksWithFish);
                    //AddHookWithFishToList(hooksWithFish);
                    CatchFishTrigger = 0;
                    GetBasketOfFish();
                }
            }
            else
            {
                timer.Stop();
                //Metod för att sluta spelet
            }

        }
        //Behövs egentligen inte
        private void AddHookWithFishToList(Dictionary<int, int> hooksWithFish)
        {
            foreach (var item in hooksWithFish)
            {
                HookHasFish.Add(item.Key, item.Value);
            }
        }

        private void UpdateHookImage(Dictionary<int, int> hooksWithFish)
        {
            for (int i = 0; i < hooksWithFish.Count; i++)
            {
                string hookId = hooksWithFish.ElementAt(i).Key.ToString();
                int arrayKey = int.Parse(hookId[2].ToString());
                int hookKey = hooksWithFish.ElementAt(i).Key;
                switch (hookKey)
                {
                    case 100:
                        ImageUri2[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);                        
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 101:
                        ImageUri2[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 102:
                        ImageUri2[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 103:
                        ImageUri2[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 104:
                        ImageUri2[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 105:
                        ImageUri2[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 106:
                        ImageUri2[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 107:
                        ImageUri2[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    default:
                        break;
                }

            }
        }

        private void GetGameView(object parameter)
        {            
            Main.Content = new GameView(player);
        }
    }
}
    
