using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public int HookId { get; set; }
        public int PositionOnIce { get; set; }
        public bool HasWorm { get; set; } = true;
        //public bool HasFish { get; set; }
        public Fish Fish { get; set; }
        //private BitmapImage _ImageSource;
        //public BitmapImage ImageSource
        //{
        //    get { return this._ImageSource; }
        //    set { this._ImageSource = value; this.OnPropertyChanged("ImageSource"); }
        //}



        public Hook()
        {
            InitializeComponent();


            string imagePath = "\\Resources\\Images\\worm.png";
            imgDynamic.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }



        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("Object", this);
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }



        //private void OnPropertyChanged(string v)
        //{
        //    // throw new NotImplementedException();
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(v));
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}