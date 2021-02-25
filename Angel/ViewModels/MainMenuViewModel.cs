using System.Windows.Input;

namespace Angel
{
    class MainMenuViewModel : BaseViewModel
    {
        #region Commands
        public ICommand PlayerViewCommand { get; set; }
        #endregion
        public MainMenuViewModel()
        {
            PlayerViewCommand = new RelayCommand(GetPlayerView, CanExecute);
            HighscoreViewCommand = new RelayCommand(GetHighscoreView, CanExecute);
            ExitGameCommand = new RelayCommand(CloseApplication, CanExecute);                
        }

        private void GetPlayerView(object parameter)
        {
            Main.Content = new PlayerView();
        }
    }
}
