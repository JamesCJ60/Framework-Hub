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
    private int _allCO;
    private int _gfxCO;
    private int _pboOffset;
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

    public int AllCO
    {
        get { return _allCO; }
        set
        {
            if (_allCO != value)
            {
                _allCO = value;
                OnPropertyChanged(nameof(AllCO));
            }
        }
    }

    public int GfxCO
    {
        get { return _gfxCO; }
        set
        {
            if (_gfxCO != value)
            {
                _gfxCO = value;
                OnPropertyChanged(nameof(GfxCO));
            }
        }
    }

    public int PboOffset
    {
        get { return _pboOffset; }
        set
        {
            if (_pboOffset != value)
            {
                _pboOffset = value;
                OnPropertyChanged(nameof(PboOffset));
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
        Temp = 100;
        AllCO = 0;
        GfxCO = 0;
        PboOffset = 0;
    }
}
