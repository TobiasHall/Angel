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
    /// Interaction logic for Hook.xaml
    /// </summary>
    public partial class Hook : UserControl
    {


        public string Name { get; set; } = "Hej hopp";
        public int HookID { get; set; }
        public Uri ImageUri33 { get; set; } 


        

        public Hook()
        {
            InitializeComponent();

            Height = 60;
            Width = 60;
            SetID();
        }

        int counter = 0;
        private void SetID()
        {
            ImageUri33 = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
        }
        
    }
}
