using Avalonia;
using Avalonia.Controls;
using Framework_Hub.Scripts.Windows.Misc;
using Framework_Hub.ViewModels;
using System;
using System.Drawing;
using System.IO;
using System.Management;
using System.Threading.Tasks;
using static Framework_Hub.Views.MainWindow;

namespace Framework_Hub.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = new MainViewModel();

        // Setup GUI
        SetUpGUI();
    }

    private void SetUpGUI()
    {
        // Create variable containing viewmodel for GUI
        var mainViewModel = new MainViewModel();

        // Setup power mode selection menu
        lbPresets.Items.Add(new SideMenu { Icon = "\ue8be", Sub = "Silent", Margin = new Thickness(0, -8, 0, -8) });
        lbPresets.Items.Add(new SideMenu { Icon = "\uec49", Sub = "Balanced", Margin = new Thickness(0, -8, 0, -8) });
        lbPresets.Items.Add(new SideMenu { Icon = "\uec4a", Sub = "Performance", Margin = new Thickness(0, -8, 0, -8) });

        // Display laptop name
        tbxLaptopName.Text = $"{GetSystemInfo.Manufacturer} {GetSystemInfo.Product}";

        // Display CPU/APU name
        tbxCpuName.Text = $"- {GetSystemInfo.GetCPUName()}";

        // Determine how many GPUs are present, their order and display their names
        if(GetSystemInfo.GetGPUName(1) != null && GetSystemInfo.GetCPUName().Contains(GetSystemInfo.GetGPUName(1).Replace("AMD", null).Replace("(TM)", null))) tbxGpuName.Text = $"- {GetSystemInfo.GetGPUName(1)} + {GetSystemInfo.GetGPUName(0)}";
        else if (GetSystemInfo.GetGPUName(1) != null) tbxGpuName.Text = $"- {GetSystemInfo.GetGPUName(0)} + {GetSystemInfo.GetGPUName(1)}";
        else tbxGpuName.Text = $"- {GetSystemInfo.GetGPUName(0)}";

        // Display RAM spec of laptop
        GetSystemInfo.GetRAMInfo(tbxRamSpecs);

        // Detect if Ryzen 9/Ryzen AI 9 is present
        if (!GetSystemInfo.GetCPUName().Contains("Ryzen 9") && !GetSystemInfo.GetCPUName().Contains("Ryzen AI 9"))
        {
            // Hide options if no Ryzen 9/Ryzen AI 9 is detected
            expCO.IsVisible = false;
            expPBO.IsVisible = false;
        }

        sdPL1.Value = mainViewModel.PL1;
        sdPL2.Value = mainViewModel.PL2;
        lbPresets.SelectedIndex = mainViewModel.PowerIndex;
    }
}
