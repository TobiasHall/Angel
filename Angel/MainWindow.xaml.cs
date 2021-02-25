using System.Windows;

namespace Angel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var view = new MainMenuView();
            Main.Content = view;
        }
    }
}
