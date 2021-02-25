using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Angel
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private GameViewModel model;
        public GameView(Player player, TimeSpan gameTimer)
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
                if (element.Fish != null)
                {
                    model.imgOfFishIsVisible = Visibility.Visible;
                    model.CollectFish(element.Fish);
                    model.Hooks.Remove(element);
                }
                else if (model.Hooks.Contains(element))
                {
                    model.imgOfFishIsVisible = Visibility.Hidden;
                    model.CollectedFishLabel = "";
                    model.Hooks.Remove(element);
                }
                else
                {
                    model.imgOfFishIsVisible = Visibility.Hidden;
                    model.CollectedFishLabel = "";
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
