using Avalonia;
using Avalonia.Controls;

namespace Framework_Hub.Views;

public partial class MainWindow : Window
{
    public class SideMenu
    {
        public string Icon { get; set; }
        public string Sub { get; set; }
        public Thickness Margin { get; set; }
    }
    public MainWindow()
    {
        InitializeComponent();

        lbSide.Items.Add(new SideMenu { Icon = "\ue80f", Sub = "Home", Margin = new Thickness(0,-8,0,-8) });
        lbSide.Items.Add(new SideMenu { Icon = "\uea80", Sub = "KBD LED", Margin = new Thickness(0,-8,0,-8) });
        lbSide.Items.Add(new SideMenu { Icon = "\ue8ab", Sub = "Auto", Margin = new Thickness(0,-8,0,-8) });
        lbSide.SelectedIndex = 0;

        this.Width = 900;
        this.Height = 500;
    }
}
