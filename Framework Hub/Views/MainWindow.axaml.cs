using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace Framework_Hub.Views;

public partial class MainWindow : Window
{
    public class SideMenu
    {
        public string Icon { get; set; }
        public string Sub { get; set; }
        public Thickness Margin { get; set; }
    }

    private UserControl currentPage;

    public MainWindow()
    {
        InitializeComponent();

        lbSide.Items.Add(new SideMenu { Icon = "\ue80f", Sub = "Home", Margin = new Thickness(0,-8,0,-8) });
        lbSide.Items.Add(new SideMenu { Icon = "\uea80", Sub = "KBD LED", Margin = new Thickness(0,-8,0,-8) });
        lbSide.Items.Add(new SideMenu { Icon = "\ue8ab", Sub = "Auto", Margin = new Thickness(0,-8,0,-8) });
        lbSide.SelectedIndex = 0;

        this.MinWidth = 975;
        this.MinHeight = 550;

        currentPage = new MainView();
        contentArea.Content = currentPage;
    }


    private bool _mouseDownForWindowMoving = false;
    private PointerPoint _originalPoint;

    private void InputElement_OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_mouseDownForWindowMoving) return;

        PointerPoint currentPoint = e.GetCurrentPoint(this);
        Position = new PixelPoint(Position.X + (int)(currentPoint.Position.X - _originalPoint.Position.X),
            Position.Y + (int)(currentPoint.Position.Y - _originalPoint.Position.Y));
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (WindowState == WindowState.Maximized || WindowState == WindowState.FullScreen) return;

        _mouseDownForWindowMoving = true;
        _originalPoint = e.GetCurrentPoint(this);
    }

    private void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _mouseDownForWindowMoving = false;
    }

    public void btnClose_OnClick(object? sender, RoutedEventArgs args)
    {
        Environment.Exit(0);
    }

    public void btnMax_OnClick(object? sender, RoutedEventArgs args)
    {
        if(this.WindowState == WindowState.Maximized) this.WindowState = WindowState.Normal;
        else this.WindowState = WindowState.Maximized;
    }

    public void btnMini_OnClick(object? sender, RoutedEventArgs args)
    {
        if (this.WindowState != WindowState.Minimized) this.WindowState = WindowState.Minimized;
    }
}
