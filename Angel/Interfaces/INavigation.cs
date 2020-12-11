using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Angel
{
    public interface INavigation
    {        public interface INavigation
        {
            public ICommand MainMenuCommand { get; set; }
            public ICommand ViewTopHighscoreCommand { get; set; }
            public ICommand ViewTopFrequentPlayersCommand { get; set; }

            void GetMainMenuView(object parameter);

            void GetHighscorePage(object parameter);
        }
    }
}
