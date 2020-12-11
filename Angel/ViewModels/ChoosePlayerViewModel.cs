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
        #region Commands
        
        public ICommand ChoosePlayerCommand { get; set; }
        #endregion
        #region Properties
        
        //public ObservableCollection<Player> PlayerList { get; set; }
        public Player SelectedPlayer { get; set; }
        #endregion

        public ChoosePlayerViewModel()
        {
            ChoosePlayerCommand = new RelayCommand(NewGame, CanExecute);
            MainMenuCommand = new RelayCommand(GetMainMenuView, CanExecute);
            //GetPlayers();
        }

        //private void GetPlayers()
        //{
        //    PlayerList = (ObservableCollection<Player>)Repository.GetDbPlayers();
        //}

        //private void CreatePlayer(object parameter)
        //{
        //    if (Nickname == null)
        //    {
        //        MessageBox.Show("You forgot to write your nickname first");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            int playerID = Repository.AddPlayer(Nickname);
        //            Player player = new Player(playerID, Nickname);
        //            GetPlayers();
        //            HighlightSelectedPlayer();
        //        }
        //        catch (PostgresException ex)
        //        {
        //            var code = ex.SqlState;
        //            MessageBox.Show($"Nickname {Nickname} already in use!");
        //        }

        //    }
        //}

        private void NewGame(object parameter)
        {
            Player player = new Player();
            //player  = SelectedPlayer;
            if (player == null)
            {
                player.Nickname = "Fräddrik";
            }
            Main.Content = new GameView(player);
        }

        //private void HighlightSelectedPlayer()
        //{
        //    if (Nickname == null)
        //    {
        //        if (PlayerList.Count == 1)
        //        {
        //            SelectedPlayer = PlayerList[0];
        //        }
        //        else
        //        {
        //            SelectedPlayer = null;
        //        }
        //    }
        //    else
        //    {
        //        GetSelectedPlayerToHiglight();
        //    }
        //}

        //private void GetSelectedPlayerToHiglight()
        //{
        //    for (int i = 0; i < PlayerList.Count; i++)
        //    {
        //        if (PlayerList[i].Nickname.ToLower() == Nickname.ToLower())
        //        {
        //            SelectedPlayer = PlayerList[i];
        //        }
        //    }
        //    Nickname = null;
        //}
        

       

        public override bool CeckIfCanExecute(object parameter)
        {
            if (SelectedPlayer != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
