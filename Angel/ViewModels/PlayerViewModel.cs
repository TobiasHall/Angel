using Npgsql;
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
        public List<string> GameTimeOptions { get; set; }

        public PlayerViewModel()
        {
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            MainMenuViewCommand = new RelayCommand(GetMainMenuView, CanExecute);

            SetGameTimeOptions();
        }
        private void SetGameTimeOptions()
        {
            GameTimeOptions = new List<string>();
            
            string one, two, three;            
            one = String.Format("{0:D2}t:{1:D2}m:{2:D2}s", 0, 30, 0 );
            two = String.Format("{0:D2}t:{1:D2}m:{2:D2}s", 1, 0, 0 );
            three = String.Format("{0:D2}t:{1:D2}m:{2:D2}s", 2, 0, 0 );
            
            GameTimeOptions.Add(one);
            GameTimeOptions.Add(two);
            GameTimeOptions.Add(three);
        }
        private void GetGameView(object parameter)
        {
            Player player = new Player();
            player.SetPlayerNickname(Nickname);
            int gameTimer = SetGameTimer();
            
            Main.Content = new GameView(MockPlayer, MockGameTime);
        }
        private int SetGameTimer()
        {
            if (SelectedGameTime == "02t:00m:00s")
            {
                return 120 *60;
            }
            else if (SelectedGameTime == "01t:00m:00s")
            {
                return 60 * 60;
            }
            else
            {
                return 30 * 60;
            }
        }
        
    }
}
