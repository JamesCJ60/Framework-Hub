using Avalonia;
using Avalonia.Controls;
using Framework_Hub.Scripts.Windows.Misc;
using Framework_Hub.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        SetUpGUI();
    }

    private void SetUpGUI()
    {
        var mainViewModel = new MainViewModel();

        lbPresets.Items.Add(new SideMenu { Icon = "\ue8be", Sub = "Silent", Margin = new Thickness(0, -8, 0, -8) });
        lbPresets.Items.Add(new SideMenu { Icon = "\uec49", Sub = "Balanced", Margin = new Thickness(0, -8, 0, -8) });
        lbPresets.Items.Add(new SideMenu { Icon = "\uec4a", Sub = "Performance", Margin = new Thickness(0, -8, 0, -8) });
        lbPresets.SelectedIndex = 2;

        tbxLaptopName.Text = $"{GetSystemInfo.Manufacturer} {GetSystemInfo.Product}";
        tbxCpuName.Text = $"- {GetSystemInfo.GetCPUName()}";

        if(GetSystemInfo.GetGPUName(1) != null && GetSystemInfo.GetCPUName().Contains(GetSystemInfo.GetGPUName(1).Replace("AMD", null).Replace("(TM)", null))) tbxGpuName.Text = $"- {GetSystemInfo.GetGPUName(1)} + {GetSystemInfo.GetGPUName(0)}";
        else if (GetSystemInfo.GetGPUName(1) != null) tbxGpuName.Text = $"- {GetSystemInfo.GetGPUName(0)} + {GetSystemInfo.GetGPUName(1)}";
        else tbxGpuName.Text = $"- {GetSystemInfo.GetGPUName(0)}";

        GetSystemInfo.GetRAMInfo(tbxRamSpecs);

        if (GetSystemInfo.Product.Contains("16"))
        {
            sdPL1.Maximum = 140;
            sdPL2.Maximum = 140;

            if (lbPresets.SelectedIndex == 1)
            {
                sdPL1.Value = 85;
                sdPL2.Value = 85;
            }
            else if (lbPresets.SelectedIndex == 1)
            {
                sdPL1.Value = 95;
                sdPL2.Value = 95;
            }
            else if (lbPresets.SelectedIndex == 2)
            {
                sdPL1.Value = 100;
                sdPL2.Value = 120;
            }
        }
    }
}
