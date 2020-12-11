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

        public ChoosePlayerViewModel()
        {
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            MainMenuCommand = new RelayCommand(GetMainMenuView, CanExecute);        
        }
        private void GetGameView(object parameter)
        {
            Player player = new Player();

            if (Nickname == null)
            {
                player.Nickname = "Fräddrik";
            }
            else
            {
                player.Nickname = Nickname;
            }
            Main.Content = new GameView(player);
        }
    }
}
