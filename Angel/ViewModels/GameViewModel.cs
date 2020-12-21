using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static Angel.GameEngine;

namespace Angel
{
    public class GameViewModel : BaseViewModel
    {
        private BitmapImage _ImageSource;
        public BitmapImage ImageSource
        {
            get { return this._ImageSource; }
            set { this._ImageSource = value; this.OnPropertyChanged("ImageSource"); }
        }

        //public BitmapImage TestTest { get; set; } = new BitmapImage(new Uri("\\Resources\\Images\\fish.png", UriKind.Relative));


        public ObservableCollection<BitmapImage> TestTest { get; set; } = new ObservableCollection<BitmapImage>() { new BitmapImage(new Uri("\\Resources\\Images\\fish.png", UriKind.Relative)), new BitmapImage(new Uri("\\Resources\\Images\\fish.png", UriKind.Relative)), new BitmapImage(new Uri("\\Resources\\Images\\fish.png", UriKind.Relative)), new BitmapImage(new Uri("\\Resources\\Images\\fish.png", UriKind.Relative)), new BitmapImage(new Uri("\\Resources\\Images\\fish.png", UriKind.Relative)), new BitmapImage(new Uri("\\Resources\\Images\\fish.png", UriKind.Relative)), new BitmapImage(new Uri("\\Resources\\Images\\fish.png", UriKind.Relative)), new BitmapImage(new Uri("\\Resources\\Images\\fish.png", UriKind.Relative)), };



        public int HookCounter { get; set; } = 100;









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
        public string GameTimer { get; set; } = "TT:MM:SS";
        public int NrOfHooks { get; set; } = 0;
        public Uri HookId100 { get; set; } = new Uri(@"/Resources/Images/hook.png", UriKind.Relative);
        public Uri FishImage { get; set; } = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
        
        //public Uri ImageUri { get; set; } = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
        //public Uri[] ImageUri2 { get; set; } = new Uri[8] {new Uri("/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative), new Uri(@"/Resources/Images/worm.png", UriKind.Relative)};
        public Dictionary<int, int> PositionOfHook { get; set; } = new Dictionary<int, int>();
        
        //public List<int> HookOnIce { get; set; }
        public Dictionary<int, int> HookHasFish { get; set; } = new Dictionary<int, int>();

        public ObservableCollection<Uri> ImageUri { get; set; } = new ObservableCollection<Uri>() { new Uri("/Resources/Images/worm.png", UriKind.Relative), new Uri("/Resources/Images/worm.png", UriKind.Relative), new Uri("/Resources/Images/worm.png", UriKind.Relative), new Uri("/Resources/Images/worm.png", UriKind.Relative), new Uri("/Resources/Images/worm.png", UriKind.Relative), new Uri("/Resources/Images/worm.png", UriKind.Relative), new Uri("/Resources/Images/worm.png", UriKind.Relative), new Uri("/Resources/Images/worm.png", UriKind.Relative), };

        DispatcherTimer timer;
        int gameTimer = 1800;
        int countdownTimer = 1800;
        int CatchFishTrigger;


        public GameViewModel(Player player, int gameTimer)
        {
            MainMenuCommand = new RelayCommand(GetMainMenuView, CanExecute);
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            LuckySnuffBtn = new RelayCommand(UseLuckySnuff, CanExecute);
            CupOfCoffeBtn = new RelayCommand(UseCupOfCoffe, CanExecute);
            this.gameTimer = gameTimer;
            countdownTimer = gameTimer;

          
            


            this.player = player;
            StartNewGame();
            LuckySnuffLabel = ExtraChanseOnTroutChansesLeft();
            CupOfCoffeLabel = ExtraChanseToCatchFishLeft();


            StartCountdown();



            string imagePath = "\\Resources\\Images\\fish.png";
            this.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));



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
                if (countdownTimer < 3600)
                {
                    GameTimer = String.Format("{0:D2}:{1:D2}:{2:D2}", countdownTimer / 3600, countdownTimer / 60, countdownTimer % 60);
                }
                else
                {
                    GameTimer = String.Format("{0:D2}:{1:D2}:{2:D2}", countdownTimer/3600, countdownTimer/120, countdownTimer % 60);
                }
                


                CatchFishTrigger++;
                CheckIfCatchFishTrigger();
            }
            else
            {
                timer.Stop();
                //Metod för att sluta spelet
            }

        }

        private void CheckIfCatchFishTrigger()
        {
            //if (CatchFishTrigger == (gameTimer / 10))
                if (CatchFishTrigger == 10)
                {                
                string imagePath = "\\Resources\\Images\\worm.png";
                this.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                TestTest[0] = new BitmapImage(new Uri("\\Resources\\Images\\worm.png", UriKind.Relative));

                Dictionary<int, int> hooksWithFish = CatchFish(PositionOfHook);
                List<Fish> fishToLabel = GetFishFromLastRound();
                UpdateLabelsInView(fishToLabel);
                                

                UpdateHookImage(hooksWithFish);
                //AddHookWithFishToList(hooksWithFish);
                CatchFishTrigger = 0;
                GetBasketOfFish();
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
                        ImageUri[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);                        
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 101:
                        ImageUri[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 102:
                        ImageUri[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 103:
                        ImageUri[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 104:
                        ImageUri[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 105:
                        ImageUri[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 106:
                        ImageUri[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    case 107:
                        ImageUri[arrayKey] = new Uri(@"/Resources/Images/fish.png", UriKind.Relative);
                        PositionOfHook.Remove(hookKey);
                        break;
                    default:
                        break;
                }

            }
        }

        private void GetGameView(object parameter)
        {            
            Main.Content = new GameView(player, gameTimer);
        }
        private void UpdateLabelsInView(List<Fish> fishToLabel)
        {
            foreach (var fish in fishToLabel)
            {
                TotalFishes++;
                TotalWeight += fish.Weight;
                if (fish.FishId == (int)FishEnum.Dace)
                {
                    NrOfDace++;
                }
                else if (fish.FishId == (int)FishEnum.Pike)
                {
                    NrOfPike++;
                }
                else if (fish.FishId == (int)FishEnum.Perch)
                {
                    NrOfPerc++;
                }
                else if (fish.FishId == (int)FishEnum.Char)
                {
                    NrOfChar++;
                }
                else
                {
                    NrOfTrout++;
                }
            }
            TotalScore = GetScore();
        }
    }
}
    
