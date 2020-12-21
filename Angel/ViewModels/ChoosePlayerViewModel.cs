using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Angel
{
    public class ChoosePlayerViewModel : BaseViewModel
    {
        public string Nickname { get; set; }
        public string SelectedGameTime { get; set; }
        public List<string> GameTimeOptions { get; set; }

        public ChoosePlayerViewModel()
        {
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            MainMenuCommand = new RelayCommand(GetMainMenuView, CanExecute);

            SetGameTimeOptions();
        }
        private void SetGameTimeOptions()
        {
            GameTimeOptions = new List<string>();
            
            string startValue, one, two, three;
            startValue = "TT:MM:SS";
            one = String.Format("{0:D2}:{1:D2}:{2:D2}", 0, 30, 0 );
            two = String.Format("{0:D2}:{1:D2}:{2:D2}", 1, 0, 0 );
            three = String.Format("{0:D2}:{1:D2}:{2:D2}", 2, 0, 0 );
            //SelectedGameTime = startValue;

            //GameTimeOptions.Add(startValue);
            GameTimeOptions.Add(one + $" ({startValue})");
            GameTimeOptions.Add(two + $" ({startValue})");
            GameTimeOptions.Add(three + $" ({startValue})");
        }
        private void GetGameView(object parameter)
        {
            Player player = new Player();
            player.SetPlayerNickname(Nickname);
            int gameTimer = SetGameTimer();
            
            Main.Content = new GameView(player);
        }
        private int SetGameTimer()
        {
            if (SelectedGameTime == "00:30:00")
            {
                return 3 *60;
            }
            else if (SelectedGameTime == "01:00:00")
            {
                return 60 * 60;
            }
            else
            {
                return 120 * 60;
            }
        }
        
    }
}
