using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Angel
{
    class MainMenuViewModel : BaseViewModel
    {
        #region Commands
        public ICommand ChoosePlayerCommand { get; set; }
        #endregion
        public MainMenuViewModel()
        {
            ChoosePlayerCommand = new RelayCommand(GetChoosePlayerPage, CanExecute);
            //ViewTopHighscoreCommand = new RelayCommand(GetHighscorePage, CanExecute);
            ExitGameCommand = new RelayCommand(CloseApplication, CanExecute);
            //MuteCommand = new RelayCommand(Mute, CanExecute);
            //StartDBConnection();
        }

        private void GetChoosePlayerPage(object parameter)
        {
            Main.Content = new ChoosePlayerView();
        }

        //private void StartDBConnection()
        //{
        //    try
        //    {
        //        Repository.StartDb();
        //    }
        //    catch
        //    {
        //        MessageBox.Show($"Couldn´t connect to the database. " +
        //            $"Check your internet connection. If the problem remains " +
        //            $"call our customer service on 1-87-ESPN-IS-KING. " +
        //            $"Our opening hours are 05:00 AM to 05:01 AM");
        //        CloseApplication(true);
        //    }
        //}

        
    }
}
