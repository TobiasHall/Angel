using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Angel
{
    public interface INavigation
    {
        public ICommand MainMenuViewCommand { get; set; }
        public ICommand NewGameCommand { get; set; }
        public ICommand ExitGameCommand { get; set; }
        public ICommand HighscoreViewCommand { get; set; }

        void GetMainMenuView(object parameter);
        void GetHighscoreView(object parameter);

    }
}
