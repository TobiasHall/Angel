using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
