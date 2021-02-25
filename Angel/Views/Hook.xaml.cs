using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace Angel
{
    /// <summary>
    /// Interaction logic for Hook.xaml
    /// </summary>
    public partial class Hook : UserControl
    {
        public int HookId { get; set; }
        public int PositionOnIce { get; set; }
        public bool PlacedOnLucyHole { get; set; }
        public bool HasWorm { get; set; } = true;        
        public Fish Fish { get; set; }
        public string imgPathWorm { get; set; } = "\\Resources\\Images\\worm.png";
        public string imgPathFish { get; set; } = "\\Resources\\Images\\fish.png";
        public string imgPathHook { get; set; } = "\\Resources\\Images\\hook.png";

        public Hook()
        {
            InitializeComponent();            
            imgDynamic.Source = new BitmapImage(new Uri(imgPathWorm, UriKind.Relative));
        }

        public void SetBaseValue(int positionOnIce)
        {
            PositionOnIce = positionOnIce;
            PlacedOnLucyHole = false;
            HasWorm = true;
            Fish = null;
            imgDynamic.Source = new BitmapImage(new Uri(imgPathWorm, UriKind.Relative));
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
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);
            // These Effects values are set in the drop target's
            // DragOver event handler.

            var hook = (Hook)e.OriginalSource;
            if (e.Effects.HasFlag(DragDropEffects.Move) && hook.Fish != null)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri(@"Resources/Cursors/fish.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move) && hook.HasWorm == false)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri(@"Resources/Cursors/hook.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move) && hook.Fish == null)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri(@"Resources/Cursors/worm.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            //else
            //{
            //    Mouse.SetCursor(Cursors.No);
            //}

            e.Handled = true;
        }

        public override string ToString()
        {
            return $"ID: {HookId}, Pos: {PositionOnIce}, Fish: {Fish}, HasWorm:{HasWorm}";
        }        
    }
}