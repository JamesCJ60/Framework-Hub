using Avalonia;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Framework_Hub.ViewModels;

public class MainViewModel : ViewModelBase
{
    private int _pl1;
    private int _pl2;
    private int _temp;

    public int Temp
    {
        get { return _temp; }
        set
        {
            if (_temp != value)
            {
                _temp = value;
                OnPropertyChanged(nameof(_temp));
            }
        }
    }

    public int PL1
    {
        get { return _pl1; }
        set
        {
            if (_pl1 != value)
            {
                _pl1 = value;
                OnPropertyChanged(nameof(PL1));
            }
        }
    }

    public int PL2
    {
        get { return _pl2; }
        set
        {
            if (_pl2 != value)
            {
                _pl2 = value;
                OnPropertyChanged(nameof(PL2));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MainViewModel()
    {
        // Set default values
        PL1 = 35;
        PL2 = 55;
        Temp = 95;
    }
}
