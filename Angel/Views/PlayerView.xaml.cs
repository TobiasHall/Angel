using System.Windows.Controls;

namespace Angel
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        public PlayerView()
        {
            InitializeComponent();
            DataContext = new PlayerViewModel();
        }
    }
}
