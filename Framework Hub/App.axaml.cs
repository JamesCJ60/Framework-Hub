using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Framework_Hub.ViewModels;
using Framework_Hub.Views;
using System.Runtime.InteropServices;
using System;
using System.Security.Principal;
using System.Diagnostics;

namespace Framework_Hub;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {

            if (IsAdmin() == false) RestartAsAdmin();
            else SetUpGUI();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            SetUpGUI();
        }
        else
        {
            Console.WriteLine("Operating system not recognised.");
            Environment.Exit(0);
        }
    }

    void SetUpGUI()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    static bool IsAdmin()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    static void RestartAsAdmin()
    {
        // Restart and run as admin
        var exeName = Process.GetCurrentProcess().MainModule.FileName;
        ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
        startInfo.Verb = "runas";
        startInfo.UseShellExecute = true;
        startInfo.Arguments = "restart";
        Process.Start(startInfo);
        Environment.Exit(0);
    }
}
