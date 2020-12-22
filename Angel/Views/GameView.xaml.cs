using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace Angel
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private GameViewModel model;
        private int counter = 100;
        public GameView(Player player, int gameTimer)
        {
            InitializeComponent();
            model = new GameViewModel(player, gameTimer);
            DataContext = model;

            for (int i = 0; i < 8; i++)
            {
                Hook hook = new Hook();
                hook.Uid = counter.ToString();
                hook.HookId = counter;                
                DropWrap.Children.Add(hook);
                counter++;
            }

            //DropWrap.Children.Add(hook);
        }

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {

        //        DataObject data = new DataObject();
        //        data.SetData("Object", this);
        //        DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
        //    }
        //}

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            //StreamResourceInfo sriCurs = ConvertPegToStream(e);
            //if (e.Effects.HasFlag(DragDropEffects.Move))
            //{
            //    Mouse.SetCursor(new Cursor(sriCurs.Stream));
            //}

            base.OnGiveFeedback(e);
            // These Effects values are set in the drop target's
            // DragOver event handler.

            var hook = (Hook)e.OriginalSource;            
            if (e.Effects.HasFlag(DragDropEffects.Move) && hook.Fish == null)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri(@"Resources/Cursors/worm.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move) && hook.Fish != null)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri(@"Resources/Cursors/fish.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            //else
            //{
            //    Mouse.SetCursor(Cursors.No);
            //}

            e.Handled = true;
        }

        //private void PanelDrop(object sender, DragEventArgs e)
        //{

        //    Panel panel = (Panel)sender;
        //    UIElement element = (UIElement)e.Data.GetData("Object");
        //    Panel parent = (Panel)VisualTreeHelper.GetParent(element);
        //    if (panel.Name != "TopGrid" && panel.Name != "DropWrap")
        //    {
        //        parent.Children.Remove(element);
        //        panel.Children.Add(element);                
        //        model.PositionOfHook.AddOrUpdate(int.Parse(element.Uid), int.Parse(panel.Uid));
        //        //model.PositionOfHook2.AddOrUpdate2((Hook)element, 2);
        //        if (element is Image)
        //        {
        //            var hej = (Image)element;
                    
        //        }
        //        var testAvBildByte = (Hook)element;
        //        model.TestAvKrok = testAvBildByte;
        //        testAvBildByte.HookId = int.Parse(element.Uid);
        //        model.TestAvKrok2.Add(testAvBildByte);
        //        //testAvBildByte.imgDynamic.Source = new BitmapImage(new Uri("\\Resources\\Images\\worm.png", UriKind.Relative));
        //        //model.HookOnIce.Add(int.Parse(panel.Uid));
        //    }
        //    else if (panel.Name == "DropWrap")
        //    {
        //        //Hook test = (Hook)element;
        //        //model.PositionOfHook2.AddOrUpdate2((Hook)element, 2);

        //        //Fungerar på en bild
        //        //string imagePath = "\\Resources\\Images\\worm.png";
        //        //model.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));



        //        parent.Children.Remove(element);
        //        panel.Children.Add(element);
        //        model.PositionOfHook.Remove(int.Parse(element.Uid));

        //    }
        //    else
        //    {
        //        parent.Children.Remove(element);
        //        panel.Children.Add(element);
        //    }


        //}
        private void PanelDrop(object sender, DragEventArgs e)
        {

            Panel panel = (Panel)sender;
            Hook element = (Hook)e.Data.GetData("Object");
            Panel parent = (Panel)VisualTreeHelper.GetParent(element);
            if (panel.Name != "TopGrid" && panel.Name != "DropWrap" && element.HasWorm == true)
            {
                parent.Children.Remove(element);
                panel.Children.Add(element);
                element.PositionOnIce = int.Parse(panel.Uid);
                model.Hooks.AddOrUpdate(element.HookId, element);
            }
            else if (panel.Name == "DropWrap")
            {
                element.HasWorm = true;
                string imagePath = "\\Resources\\Images\\worm.png";
                element.imgDynamic.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                model.CollectFish(element.Fish);                
                element.Fish = null;

                parent.Children.Remove(element);
                panel.Children.Add(element);

            }
            else
            {
                parent.Children.Remove(element);
                panel.Children.Add(element);
            }


        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject();
            data.SetData("Object", sender);
            DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Move);

        }
    }
}
