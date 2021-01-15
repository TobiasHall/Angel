﻿using System;
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
        public List<Hook> Hooks { get; set; } = new List<Hook>();        
        public List<Fish> CollectedFishes { get; private set; } = new List<Fish>();
        public string CollectedFishLabel { get; set; }
        public ICommand LuckySnuffBtn { get; set; }
        public ICommand CupOfCoffeBtn { get; set; }
        public ICommand EndViewCommand { get; set; }
        public bool LuckySnuffBtnEnabled { get; set; } = true;
        public string LuckySnuffLabel { get; set; } = "Tursnus kvar: ";
        public bool CupOfCoffeBtnEnabled { get; set; } = true;
        public string CupOfCoffeLabel { get; set; }
        public string TotalScoreLabel { get; set; } = "Poäng: 0";
        public string Nickname { get; set; }
        private Player player { get; set; }
        public string GameTimer { get; set; } = "Kör!";
        public bool IceHolesIsEnabled { get; set; } = true;
        public Uri imgSourceOfFish { get; set; }
        public Visibility imgOfFishIsVisible { get; set; }

        DispatcherTimer timer;
        int gameTimer = 1800;
        int countdownTimer = 1800;
        int catchFishTrigger;

        string imagePathWorm = "\\Resources\\Images\\worm.png";
        string imagePathFish = "\\Resources\\Images\\fish.png";
        string imagePathHook = "\\Resources\\Images\\hook.png";

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
            this.player = player;
            Nickname = player.Nickname;
            LuckySnuffLabel = $"{snuffString} {numbersOfExtraChansOnTrout}";
            CupOfCoffeLabel = $"{coffeString} {numbersOfExtraChansToCatchFish}";
            StartCountdown();
        }
        private void GetGameView(object parameter)
        {
            timer.Stop();
            Main.Content = new  GameView(player, gameTimer);
            //MessageBoxResult result = MessageBox.Show($"Vill du starta om fisketur?", "Varning", MessageBoxButton.YesNo);
            //switch (result)
            //{
            //    case MessageBoxResult.Yes:
            //        break;
            //    case MessageBoxResult.No:
            //        timer.Start();
            //        break;
            //}            
        }
        private void GetEndView(object parameter)
        {
            timer.Stop();
            FishingRoundEnded();
            Main.Content = new EndView(player, gameTimer);
            //MessageBoxResult result = MessageBox.Show($"Vill du avsluta fisketur?", "Varning", MessageBoxButton.YesNo);
            //switch (result)
            //{
            //    case MessageBoxResult.Yes:
            //        break;
            //    case MessageBoxResult.No:
            //        timer.Start();
            //        break;
            //}            
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
            if (countdownTimer != 0)
            {
                countdownTimer--;
                catchFishTrigger++;
                SetGameTimerLabel();

                if (catchFishTrigger == (gameTimer / 10))
                {
                    DeleteFish();
                    CheckIfCatchFishTrigger();
                }
            }
            else
            {
                //Metod för att sluta spelet
                timer.Stop();
                IceHolesIsEnabled = false;

                player.FishBucket = CollectedFishes;
                player.SetPlayerScore(Score);
                GetEndView(player);

            }

        }

        private void SetGameTimerLabel()
        {
            GameTimer = countdownTimer.ToString();
            if (countdownTimer < 3600)
            {
                GameTimer = String.Format("{0:D2}:{1:D2}:{2:D2}", countdownTimer / 3600, countdownTimer / 60, countdownTimer % 60);
            }
            else
            {
                GameTimer = String.Format("{0:D2}:{1:D2}:{2:D2}", countdownTimer / 3600, countdownTimer / 120, countdownTimer % 60);
            }
        }

        private void DeleteFish()
        {
            for (int i = 0; i < Hooks.Count; i++)
            {
                if (Hooks[i].Fish != null)
                {
                    Hooks[i].Fish = null;
                    Hooks[i].imgDynamic.Source = new BitmapImage(new Uri(imagePathHook, UriKind.Relative));
                    Hooks.Remove(Hooks[i]);                    
                }
            }
        }

        private void CheckIfCatchFishTrigger()
        {

            Hooks = CatchFish(Hooks);
            PlaySoundEffect();
            for (int i = 0; i < Hooks.Count; i++)
            {
                if (Hooks[i].Fish != null)
                {
                    Hooks[i].imgDynamic.Source = new BitmapImage(new Uri(imagePathFish, UriKind.Relative));
                }
                else if (Hooks[i].HasWorm == false)
                {
                    Hooks[i].imgDynamic.Source = new BitmapImage(new Uri(imagePathHook, UriKind.Relative));
                    Hooks.Remove(Hooks[i]);
                }
            }
            catchFishTrigger = 0;


        }
        private void UpdateLabelsInView(Fish fish)
        {
            CollectedFishLabel = $"{fish.Species}: {fish.Weight}g";
        }
    }
}
    
