﻿using System;
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
        public GameView(Player player)
        {
            InitializeComponent();
            model = new GameViewModel(player);
            DataContext = model;            
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
            if (e.Effects.HasFlag(DragDropEffects.Copy))
            {
                Mouse.SetCursor(Cursors.Cross);
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move))
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

        private void PanelDrop(object sender, DragEventArgs e)
        {

            Panel panel = (Panel)sender;
            UIElement element = (UIElement)e.Data.GetData("Object");
            Panel parent = (Panel)VisualTreeHelper.GetParent(element);
            if (panel.Name != "TopGrid" && panel.Name != "DropWrap")
            {
                parent.Children.Remove(element);
                panel.Children.Add(element);                
                model.PositionOfHook.AddOrUpdate(int.Parse(element.Uid), int.Parse(panel.Uid));
                //model.PositionOfHook2.AddOrUpdate2((Hook)element, 2);
                if (element is Image)
                {
                    var hej = (Image)element;
                    
                }
                //model.HookOnIce.Add(int.Parse(panel.Uid));
            }
            else if (panel.Name == "DropWrap")
            {
                //Hook test = (Hook)element;
                //model.PositionOfHook2.AddOrUpdate2((Hook)element, 2);

                //Fungerar på en bild
                //string imagePath = "\\Resources\\Images\\worm.png";
                //model.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));



                parent.Children.Remove(element);
                panel.Children.Add(element);
                model.PositionOfHook.Remove(int.Parse(element.Uid));

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
            DragDrop.DoDragDrop(this, data, DragDropEffects.Move);

        }
    }
}
