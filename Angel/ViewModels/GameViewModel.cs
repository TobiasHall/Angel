using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
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
        public ICommand LuckySnuffBtn { get; set; }
        public ICommand CupOfCoffeBtn { get; set; }
        public ICommand EndViewCommand { get; set; }
        public List<Hook> Hooks { get; set; } = new List<Hook>();        
        public List<Fish> CollectedFishes { get; private set; } = new List<Fish>();
        public string CollectedFishLabel { get; set; }
        public bool LuckySnuffBtnEnabled { get; set; } = true;
        public string LuckySnuffLabel { get; set; } = "Tursnus kvar: ";
        public bool CupOfCoffeBtnEnabled { get; set; } = true;
        public string CupOfCoffeLabel { get; set; }
        public string TotalScoreLabel { get; set; } = "Poäng: 0";
        public string NicknameLabel { get; set; }
        public string GameTimerLabel { get; set; } = "Kör!";
        public bool IceHolesIsEnabled { get; set; } = true;
        public Uri imgSourceOfFish { get; set; } = new Uri(@"/Resources/Images/Fishes/pike.png", UriKind.Relative);
        public Visibility imgOfFishIsVisible { get; set; } = Visibility.Hidden;

        private Player player { get; set; }
        DispatcherTimer timer;
        int gameTimer;
        int countdownTimer;
        int catchFishTrigger;

        string snuffString = "Tursnus kvar:";
        string coffeString = "Kaffe kvar:";

        public GameViewModel(Player player, int gameTimer)
        {
            MainMenuViewCommand = new RelayCommand(GetMainMenuView, CanExecute);
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            ExitGameCommand = new RelayCommand(CloseApplication, CanExecute);
            EndViewCommand = new RelayCommand(GetEndView, CanExecute);

            LuckySnuffBtn = new RelayCommand(UseLuckySnuff, CanExecute);
            CupOfCoffeBtn = new RelayCommand(UseCupOfCoffe, CanExecute);
            StartNewGame();
            this.gameTimer = gameTimer;
            countdownTimer = gameTimer;
            //GameTimerLabel = TimeSpan.FromSeconds((double)countdownTimer).ToString();
            this.player = player;
            NicknameLabel = player.Nickname;
            LuckySnuffLabel = $"{snuffString} {numbersOfExtraChansOnTrout}";
            CupOfCoffeLabel = $"{coffeString} {numbersOfExtraChansToCatchFish}";
            StartCountdown();
        }
        private void GetGameView(object parameter)
        {
            timer.Stop();
            Main.Content = new  GameView(player, gameTimer);
        }
        private void GetEndView(object parameter)
        {
            if (timer.IsEnabled)
            {
                FishingRoundEnded();
            }
            Main.Content = new EndView(player, gameTimer);
        }
        private void PlaySoundEffect()
        {
            foreach (var hook in Hooks)
            {
                if (hook.Fish != null)
                {
                    MediaHelper.PlayMedia(MediaHelper.soundEffectPlayer, new Uri(@"Resources/Sounds/whiplash.mp3", UriKind.Relative));
                    break;
                }
            }
        }
        private void FishingRoundEnded()
        {
            timer.Stop();
            player.FishBucket = CollectedFishes;
            player.SetPlayerScore(Score);

        }
        public void CollectFish(Fish fish)
        {
            //TODO: Fixa en tom bild till bild av fisk som en fallback
            if (fish != null)
            {         
                imgSourceOfFish = fish.imagePathFish;
                CollectedFishes.Add(fish);
                AddToScore(fish);
                UpdateLabelsInView(fish);
            }
        }
        private void UseLuckySnuff(object parameter)
        {
            LuckySnuffBtnEnabled = ExtraChanseOnTrout();
            LuckySnuffLabel = $"{snuffString} {numbersOfExtraChansOnTrout}";
        }
        private void UseCupOfCoffe(object parameter)
        {
            CupOfCoffeBtnEnabled = ExtraChanseToCatchFish();
            CupOfCoffeLabel = $"{coffeString} {numbersOfExtraChansToCatchFish}";
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
            countdownTimer--;
            catchFishTrigger++;
            if (countdownTimer > 0)
            {
                GameTimerLabel = TimeSpan.FromSeconds((double)countdownTimer).ToString();

                if (catchFishTrigger == (gameTimer / 10))
                {
                    DeleteFish();
                    CheckIfCatchFishTrigger();
                }
            }
            else if (countdownTimer == 0)
            {
                GameTimerLabel = "Slut!";
                IceHolesIsEnabled = false;
            }
            else
            {
                FishingRoundEnded();
                GetEndView(player);
            }
        }

        private void DeleteFish()
        {
            List<Hook> tempHooks = new List<Hook>(Hooks);
            foreach (var hook in tempHooks)
            {
                int index = Hooks.IndexOf(hook);
                if (hook.Fish != null)
                {
                    Hooks[index].Fish = null;
                    Hooks[index].imgDynamic.Source = new BitmapImage(new Uri(Hooks[index].imgPathHook, UriKind.Relative));
                    Hooks.Remove(Hooks[index]);
                }
                else if (hook.HasWorm == false)
                {
                    Hooks.RemoveAt(index);
                }
            }
        }

        private void CheckIfCatchFishTrigger()
        {
            var tempHooks = CatchFish(Hooks);
            PlaySoundEffect();
            ChangeImageSourceOfHook(tempHooks);
            
            catchFishTrigger = 0;
        }
        private void ChangeImageSourceOfHook(List<Hook> tempHooks)
        {
            foreach (var hook in tempHooks)
            {
                int index = Hooks.IndexOf(hook);
                if (hook.Fish != null)
                {
                    Hooks[index].imgDynamic.Source = new BitmapImage(new Uri(Hooks[index].imgPathFish, UriKind.Relative));
                }
                else if (hook.HasWorm == false)
                {
                    Hooks[index].imgDynamic.Source = new BitmapImage(new Uri(Hooks[index].imgPathHook, UriKind.Relative));
                    //Hooks.RemoveAt(index);
                }
            }
        }
        private void UpdateLabelsInView(Fish fish)
        {
            TotalScoreLabel = $"Poäng: {Score}";
            CollectedFishLabel = $"{fish.Species}: {fish.Weight}g";
        }
    }
}
    
