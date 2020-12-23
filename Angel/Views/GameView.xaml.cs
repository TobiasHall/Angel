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
        
        private void PanelDrop(object sender, DragEventArgs e)
        {

            Panel panel = (Panel)sender;
            Hook element = (Hook)e.Data.GetData("Object");
            Panel parent = (Panel)VisualTreeHelper.GetParent(element);
            if (panel.Name != "TopGrid" && panel.Name != "FishAndHookDropZone" && element.HasWorm == true)
            {
                parent.Children.Remove(element);
                panel.Children.Add(element);
                
                if (model.Hook2.Contains(element))
                {
                    element.PositionOnIce = int.Parse(panel.Uid);
                }
                else
                {
                    element.PositionOnIce = int.Parse(panel.Uid);
                    model.Hook2.Add(element);
                }               
            }
            else if (panel.Name == "FishAndHookDropZone")
            {
                if (model.Hook2.Contains(element))
                {
                    model.Hook2.Remove(element);
                }
                else
                {
                    element.HasWorm = true;
                    element.imgDynamic.Source = new BitmapImage(new Uri("\\Resources\\Images\\worm.png", UriKind.Relative));
                    model.CollectFish(element.Fish);
                }

                element.PositionOnIce = int.Parse(panel.Uid);                    
                parent.Children.Remove(element);
                panel.Children.Add(element);
            }
            else
            {
                parent.Children.Remove(element);
                panel.Children.Add(element);
            }
        }
    }
}
