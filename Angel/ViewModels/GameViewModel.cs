﻿using System;
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
        public List<Hook> Hook2 { get; set; } = new List<Hook>();        
        public List<Fish> CollectedFishes { get; private set; } = new List<Fish>();
        public string CollectedFishSpeciesLbl { get; set; }
        public int CollectedFishWeightLbl { get; set; }
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
        public int TotalWeight { get; set; } = 0;
        public int TotalScore { get; set; }
        public Player player { get; set; }
        public string GameTimer { get; set; } = "TT:MM:SS";
        
        DispatcherTimer timer;
        int gameTimer = 1800;
        int countdownTimer = 1800;
        int CatchFishTrigger;

        string imagePathWorm = "\\Resources\\Images\\worm.png";
        string imagePathFish = "\\Resources\\Images\\fish.png";

        public GameViewModel(Player player, int gameTimer)
        {
            MainMenuCommand = new RelayCommand(GetMainMenuView, CanExecute);
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            LuckySnuffBtn = new RelayCommand(UseLuckySnuff, CanExecute);
            CupOfCoffeBtn = new RelayCommand(UseCupOfCoffe, CanExecute);
            StartNewGame();
            this.gameTimer = gameTimer;
            countdownTimer = gameTimer;
            this.player = player;
            LuckySnuffLabel = numbersOfExtraChansOnTrout;
            CupOfCoffeLabel = numbersOfExtraChansToCatchFish;
            StartCountdown();
        }
        private void GetGameView(object parameter)
        {
            Main.Content = new GameView(player, gameTimer);
        }
        public void CollectFish(Fish fish)
        {
            if (fish != null)
            {
                CollectedFishes.Add(fish);
                AddToScore(fish);
                UpdateLabelsInView(fish);
                fish = null;
            }
        }
        private void UseLuckySnuff(object parameter)
        {
            LuckySnuffBtnEnabled = ExtraChanseOnTrout();
            LuckySnuffLabel = numbersOfExtraChansOnTrout;
        }
        private void UseCupOfCoffe(object parameter)
        {
            CupOfCoffeBtnEnabled = ExtraChanseToCatchFish();
            CupOfCoffeLabel = numbersOfExtraChansToCatchFish;
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
            //TODO: Fixa så att den inte slår ut på noll
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
            if (CatchFishTrigger == (gameTimer / 10))
            {
                CatchFish(Hook2);
                for (int i = 0; i < Hook2.Count; i++)
                {
                    if (Hook2[i].Fish != null)
                    {
                        Hook2[i].imgDynamic.Source = new BitmapImage(new Uri(imagePathFish, UriKind.Relative));
                        Hook2[i].HasWorm = false;
                        Hook2.Remove(Hook2[i]);
                    }
                }
                CatchFishTrigger = 0;
                GetBasketOfFish();
            }
        }
        private void UpdateLabelsInView(Fish fish)
        {            
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
            TotalFishes++;
            TotalWeight += fish.Weight;
            CollectedFishSpeciesLbl = fish.Species;
            CollectedFishWeightLbl = fish.Weight;
            TotalScore = Score;
        }
    }
}
    
