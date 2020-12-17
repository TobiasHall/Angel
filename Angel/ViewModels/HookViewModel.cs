using System;
using System.Collections.Generic;
using System.Text;

namespace Angel
{
    public class HookViewModel : BaseViewModel
    {
        public Uri ImageUri3333 { get; set; } = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
        private Uri[] test = new Uri[8];
        private int counter = 0;


        public HookViewModel()
        {
            test[counter] = new Uri(@"/Resources/Images/worm.png", UriKind.Relative);
            counter++;
        }
        
    }
}
