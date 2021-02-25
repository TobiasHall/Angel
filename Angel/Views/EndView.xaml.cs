using System;
using System.Windows.Controls;

namespace Angel
{
    /// <summary>
    /// Interaction logic for EndView.xaml
    /// </summary>
    public partial class EndView : UserControl
    {        
        public EndView(Player player, TimeSpan gameTimer)
        {            
            InitializeComponent();
            DataContext = new EndViewModel(player, gameTimer);
        }
    }
}
