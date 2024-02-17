using Avalonia;
using Avalonia.Controls;
using static Framework_Hub.Views.MainWindow;

namespace Framework_Hub.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        lbPresets.Items.Add(new SideMenu { Icon = "\ue8be", Sub = "Silent", Margin = new Thickness(0, -8, 0, -8) });
        lbPresets.Items.Add(new SideMenu { Icon = "\uec49", Sub = "Balanced", Margin = new Thickness(0, -8, 0, -8) });
        lbPresets.Items.Add(new SideMenu { Icon = "\uec4a", Sub = "Performance", Margin = new Thickness(0, -8, 0, -8) });
        lbPresets.SelectedIndex = 2;
    }
}
