using System;
using System.Collections.Generic;
using System.Text;
using static Angel.DataSaverExtension;

namespace Angel
{
    public class HighscoreViewModel : BaseViewModel
    {
        public List<Player> HighscoreList { get; set; }
        public int Score { get; set; }
        public string Nickname { get; set; }
        public HighscoreViewModel()
        {
            MainMenuViewCommand = new RelayCommand(GetMainMenuView, CanExecute);
            ExitGameCommand = new RelayCommand(CloseApplication, CanExecute);
            GetHighscoreList();
        }

        private void GetHighscoreList()
        {
            HighscoreList = ReadFromBinaryFile<List<Player>>("hsl.bin");
        }

        public HighscoreViewModel(Player player)
        {
            Score = player.Score;
            Nickname = player.Nickname;
        }
    }
}
