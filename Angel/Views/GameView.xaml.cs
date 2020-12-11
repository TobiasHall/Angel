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
                Mouse.SetCursor(Cursors.ArrowCD);
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }

            e.Handled = true;
        }

        private void PanelDrop(object sender, DragEventArgs e)
        {

            Panel panel = (Panel)sender;
            UIElement element = (UIElement)e.Data.GetData("Object");
            Panel parent = (Panel)VisualTreeHelper.GetParent(element);
            if (panel.Name != "TopGrid")
            {
                parent.Children.Remove(element);
                panel.Children.Add(element);

            
                //TODO: Fixa så att man kan flytta runt på brädan och att PositionOfHook uppdateras korrekt
                model.PositionOfHook.AddOrUpdate(int.Parse(panel.Uid), int.Parse(element.Uid));
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
