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
        string imagePathWorm = "\\Resources\\Images\\worm.png";
        string imagePathFish = "\\Resources\\Images\\fish.png";
        string imagePathHook = "\\Resources\\Images\\hook.png";
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

                Thickness margin = hook.Margin;
                margin.Right = 6;
                margin.Left = 6;
                hook.Margin = margin;

                FishAndHookDropZone.Children.Add(hook);
                counter++;
            }
        }
        
        private void PanelDrop(object sender, DragEventArgs e)
        {
            //ContentControl cont = (ContentControl)sender;
            Panel panel = (Panel)sender;
            Hook element = (Hook)e.Data.GetData("Object");
            Panel parent = (Panel)VisualTreeHelper.GetParent(element);
            if (panel.Name != "TopGrid" && panel.Name != "FishAndHookDropZone" && element.HasWorm == true)
            {
                if (panel.Children.Count == 0)
                {
                    parent.Children.Remove(element);
                    panel.Children.Add(element);
                
                    if (model.Hooks.Contains(element))
                    {
                        element.PositionOnIce = int.Parse(panel.Uid);
                    }
                    else
                    {
                        element.PositionOnIce = int.Parse(panel.Uid);
                        model.Hooks.Add(element);
                    }
                }
            }
            else if (panel.Name == "FishAndHookDropZone")
            {
                //TODO: Ändra så att den kollar om kroken har en fisk. På så vis kan jag göra som innan att en fisk kan slita sig med EraseFishFromHook
                if (element.Fish != null)
                {
                    model.CollectFish(element.Fish);
                    model.Hooks.Remove(element);
                }
                else if (model.Hooks.Contains(element))
                {
                    model.Hooks.Remove(element);
                }

                element.SetBaseValue(int.Parse(panel.Uid));                
                parent.Children.Remove(element);
                panel.Children.Add(element);
            }
            else
            {
                element.PositionOnIce = int.Parse(panel.Uid);
                parent.Children.Remove(element);
                panel.Children.Add(element);
            }
        }
    }
}
