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
        public TimeSpan SelectedGameTime { get; set; }
        public List<TimeSpan> GameTimeOptions { get; set; }

        Player player;
        
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
            
            UseMockData(true);
            
            Main.Content = new GameView(player, SelectedGameTime);
        }
        private void UseMockData(bool useMock)
        {
            if (useMock)
            {
                MockData mockData = new MockData();
                player = MockPlayer;
                SelectedGameTime = MockGameTime;
            }
        }
    }
}
