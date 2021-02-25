using System.Windows.Controls;

namespace Angel
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        private MainMenuViewModel model;

        public MainMenuView()
        {
            InitializeComponent();
            model = new MainMenuViewModel();
            DataContext = model;            
        }
    }
}
