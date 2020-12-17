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
        public Uri ImageUri33 { get; set; } = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
        public string Test { get; set; } = "";
        public int IceDrillHolePosition { get; set; }
        public bool HasFishOnHook { get; set; }
        public bool HasNoMaggotOnHook { get; set; }


        private HookViewModel model;

        

        public Hook()
        {
            InitializeComponent();

            model = new HookViewModel();
            DataContext = model;


            Height = 60;
            Width = 60;
            //SetID();
        }

        int counter = 0;
        private void SetID()
        {
            ImageUri33 = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
        }
        
    }
}
