using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Angel
{
    public class FishingHook : UserControl
    {
        public Uri ImageUri44 { get; set; }
        public FishingHook()
        {            
            Height = 60;
            Width = 80;
        }


        private void SetID()
        {
            ImageUri44 = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
        }
    }
}
