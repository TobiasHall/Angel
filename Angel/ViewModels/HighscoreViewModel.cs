using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static Angel.DataSaverExtension;

namespace Angel
{
    public class HighscoreViewModel : BaseViewModel
    {
        public ICommand ResetHighscoreCommand { get; set; }
        public List<Player> HighscoreList { get; set; }
        public int TotalScore { get; set; }
        public string Nickname { get; set; }

        private Highscore highscore = new Highscore();
        public HighscoreViewModel()
        {
            MainMenuViewCommand = new RelayCommand(GetMainMenuView, CanExecute);
            ExitGameCommand = new RelayCommand(CloseApplication, CanExecute);
            ResetHighscoreCommand = new RelayCommand(DeleteHighscoreFile, CanExecute);
            GetHighscoreList();
        }

        private void DeleteHighscoreFile(object parameter)
        {
            highscore.DeletePlayersFile();
            HighscoreList = new List<Player>();
        }

        private void GetHighscoreList()
        {
            HighscoreList = highscore.Players;
            if (HighscoreList.Count != 0)
            {
                TotalScore = highscore.LatestRoundPlayer.Score;
                Nickname = highscore.LatestRoundPlayer.Nickname;
            }
            else
            {
                TotalScore = 0;
                Nickname = "-";
            }
        }

        public HighscoreViewModel(Player player)
        {
            TotalScore = player.Score;
            Nickname = player.Nickname;
        }
    }
}
