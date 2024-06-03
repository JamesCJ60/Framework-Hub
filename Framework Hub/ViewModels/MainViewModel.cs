using Avalonia;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;

namespace Framework_Hub.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private int _pl1;
        private int _pl2;
        private int _temp;
        private int _allCO;
        private int _gfxCO;
        private int _pboOffset;
        private int _winPower;
        private string _winPowerText;

        [Reactive]
        public int Temp
        {
            get => _temp;
            set => this.RaiseAndSetIfChanged(ref _temp, value);
        }

        [Reactive]
        public int PL1
        {
            get => _pl1;
            set => this.RaiseAndSetIfChanged(ref _pl1, value);
        }

        [Reactive]
        public int PL2
        {
            get => _pl2;
            set => this.RaiseAndSetIfChanged(ref _pl2, value);
        }

        [Reactive]
        public int AllCO
        {
            get => _allCO;
            set => this.RaiseAndSetIfChanged(ref _allCO, value);
        }

        [Reactive]
        public int GfxCO
        {
            get => _gfxCO;
            set => this.RaiseAndSetIfChanged(ref _gfxCO, value);
        }

        [Reactive]
        public int PboOffset
        {
            get => _pboOffset;
            set => this.RaiseAndSetIfChanged(ref _pboOffset, value);
        }

        [Reactive]
        public int WinPower
        {
            get => _winPower;
            set => this.RaiseAndSetIfChanged(ref _winPower, value);
        }

        [Reactive]
        public string WinPowerText
        {
            get => _winPowerText;
            set => this.RaiseAndSetIfChanged(ref _winPowerText, value);
        }

        public MainViewModel()
        {
            // Set default values
            PL1 = 35;
            PL2 = 55;
            Temp = 100;
            AllCO = 0;
            GfxCO = 0;
            PboOffset = 1;
            WinPower = 2;
            WinPowerText = "\uec4a";

            this.WhenAnyValue(
                x => x.Temp,
                x => x.PL1,
                x => x.PL2,
                x => x.AllCO,
                x => x.GfxCO,
                x => x.PboOffset,
                x => x.WinPower
            ).Subscribe(_ => OnPower());
        }

        private void OnPower()
        {
            if (WinPower == 0) WinPowerText = "\ue8be";
            else if (WinPower == 1) WinPowerText = "\uec49";
            else if (WinPower == 2) WinPowerText = "\uec4a";
        }
    }
}
