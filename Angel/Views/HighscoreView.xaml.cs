using System.Windows.Controls;

namespace Angel
{
    /// <summary>
    /// Interaction logic for HighscoreView.xaml
    /// </summary>
    public partial class HighscoreView : UserControl
    {
        public HighscoreView()
        {
            InitializeComponent();
            DataContext = new HighscoreViewModel();
        }
    }
}
