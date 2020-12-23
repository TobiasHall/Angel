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
        public GameView(Player player, int gameTimer)
        {
            InitializeComponent();
            model = new GameViewModel(player, gameTimer);
            DataContext = model;
            CreateHooks();
            
        }
        private void CreateHooks()
        {
            int counter = 100;
            for (int i = 0; i < 8; i++)
            {
                Hook hook = new Hook();
                hook.Uid = counter.ToString();
                hook.HookId = counter;
                FishAndHookDropZone.Children.Add(hook);
                counter++;
            }
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
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
        private void PanelDrop(object sender, DragEventArgs e)
        {

            Panel panel = (Panel)sender;
            Hook element = (Hook)e.Data.GetData("Object");
            Panel parent = (Panel)VisualTreeHelper.GetParent(element);
            if (panel.Name != "TopGrid" && panel.Name != "FishAndHookDropZone" && element.HasWorm == true)
            {
                parent.Children.Remove(element);
                panel.Children.Add(element);
                element.PositionOnIce = int.Parse(panel.Uid);
                model.Hooks.AddOrUpdate(element.HookId, element);
            }
            else if (panel.Name == "FishAndHookDropZone")
            {
                element.HasWorm = true;
                string imagePath = "\\Resources\\Images\\worm.png";
                element.imgDynamic.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                model.CollectFish(element.Fish);                
                element.Fish = null;

                parent.Children.Remove(element);
                panel.Children.Add(element);

                if (model.Hooks.ContainsKey(element.HookId))
                {
                    model.Hooks.Remove(element.HookId);
                }

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
