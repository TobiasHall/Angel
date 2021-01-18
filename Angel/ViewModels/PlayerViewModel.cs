using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using static Angel.MockData;

namespace Angel
{
    public class PlayerViewModel : BaseViewModel
    {
        public string Nickname { get; set; }
        public string SelectedGameTime { get; set; }
        public List<TimeSpan> GameTimeOptions { get; set; }

        Player player;
        int gameTimer;
        public PlayerViewModel()
        {
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            MainMenuViewCommand = new RelayCommand(GetMainMenuView, CanExecute);

            SetGameTimeOptions();
        }
        private void SetGameTimeOptions()
        {
            GameTimeOptions = new List<TimeSpan>();
            
            TimeSpan optOne, optTwo, optThree, optFour, optFive;
            GameTimeOptions.Add(optOne = TimeSpan.FromHours(0.5));
            GameTimeOptions.Add(optTwo = TimeSpan.FromHours(1));
            GameTimeOptions.Add(optThree = TimeSpan.FromHours(2));
            GameTimeOptions.Add(optFour = TimeSpan.FromHours(4));
            GameTimeOptions.Add(optFive = TimeSpan.FromHours(8));
        }
        private void GetGameView(object parameter)
        {
            player = new Player();
            player.SetPlayerNickname(Nickname);
            gameTimer = SetGameTimer();
            
            UseMockData(true);
            
            Main.Content = new GameView(player, gameTimer);
        }
        private int SetGameTimer()
        {
            if (SelectedGameTime == "02:00:00")
            {
                return 2 * 60 * 60;
            }
            else if (SelectedGameTime == "01:00:00")
            {
                return 60 * 60;
            }
            else if (SelectedGameTime == "04:00:00")
            {
                return 4 * 60 * 60;
            }
            else if (SelectedGameTime == "08:00:00")
            {
                return 8 * 60 * 60;
            }
            else
            {
                return 30 * 60;
            }
        }
        private void UseMockData(bool useMock)
        {
            if (useMock)
            {
                MockData mockData = new MockData();
                player = MockPlayer;
                gameTimer = MockGameTime;
            }
        }
    }
}
