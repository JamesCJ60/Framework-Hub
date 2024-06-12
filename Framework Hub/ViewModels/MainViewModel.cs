using Avalonia;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;
using Framework_Hub.Scripts.Windows.RyzenAdj;
using Framework_Hub.Scripts.Linux.RyzenAdj;
using System.Runtime.InteropServices;
using Framework_Hub.Scripts.Windows.Misc;

namespace Framework_Hub.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private int _powerIndex;

        private int _pl1 = 999;
        private int _pl2 = 999;

        private int _pl1Max = 999;
        private int _pl2Max = 999;
        private int _pl1Min = 999;
        private int _pl2Min = 999;

        private int _temp;

        private int _allCO;
        private int _gfxCO;

        private int _pboOffset;

        private int _winPower;
        private string _winPowerText;

        [Reactive]
        public int PowerIndex
        {
            get => _powerIndex;
            set => this.RaiseAndSetIfChanged(ref _powerIndex, value);
        }

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

        public int PL1Max
        {
            get => _pl1Max;
            set => this.RaiseAndSetIfChanged(ref _pl1Max, value);
        }

        [Reactive]
        public int PL2Max
        {
            get => _pl2Max;
            set => this.RaiseAndSetIfChanged(ref _pl2Max, value);
        }

        public int PL1Min
        {
            get => _pl1Min;
            set => this.RaiseAndSetIfChanged(ref _pl1Min, value);
        }

        [Reactive]
        public int PL2Min
        {
            get => _pl2Max;
            set => this.RaiseAndSetIfChanged(ref _pl2Min, value);
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
            // Set event for power related value changes 
            this.WhenAnyValue(
                x => x.Temp,
                x => x.PL1,
                x => x.PL2,
                x => x.AllCO,
                x => x.GfxCO,
                x => x.PboOffset,
                x => x.WinPower
            ).Subscribe(_ => OnPowerChange());

            PowerIndex = 2;
            Temp = 100;

            // Setup temp defaults for each power mode
            SetUpTempPower();
        }
        int runs = 0;
        private void OnPowerChange()
        {
            // Update Windows power mode icon
            if (WinPower == 0) WinPowerText = "\ue8be";
            else if (WinPower == 1) WinPowerText = "\uec49";
            else if (WinPower == 2) WinPowerText = "\uec4a";

            // RyzenAdj apply code for Windows 
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Set temp limit
                RyzenAdj_Backend_Windows.set_tctl_temp(RyzenAdj_Backend_Windows.ry, (uint)Temp);
                // Set PL1
                RyzenAdj_Backend_Windows.set_slow_limit(RyzenAdj_Backend_Windows.ry, (uint)(PL1 * 1000));
                // Set PL2
                RyzenAdj_Backend_Windows.set_fast_limit(RyzenAdj_Backend_Windows.ry, (uint)(PL2 * 1000));

                // Set all core Curve Optimiser offset
                RyzenAdj_Backend_Windows.set_coall(RyzenAdj_Backend_Windows.ry, (uint)AllCO);
                // Set iGPU Curve Optimiser offset
                RyzenAdj_Backend_Windows.set_cogfx(RyzenAdj_Backend_Windows.ry, (uint)GfxCO);
            }
            // RyzenAdj apply code for Linux 
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Set temp limit
                RyzenAdj_Backend_Linux.set_tctl_temp(RyzenAdj_Backend_Linux.ry, (uint)Temp);
                // Set PL1
                RyzenAdj_Backend_Linux.set_slow_limit(RyzenAdj_Backend_Linux.ry, (uint)(PL1 * 1000));
                // Set PL2
                RyzenAdj_Backend_Linux.set_fast_limit(RyzenAdj_Backend_Linux.ry, (uint)(PL2 * 1000));

                // Set all core Curve Optimiser offset
                RyzenAdj_Backend_Linux.set_coall(RyzenAdj_Backend_Linux.ry, (uint)AllCO);
                // Set iGPU Curve Optimiser offset
                RyzenAdj_Backend_Linux.set_cogfx(RyzenAdj_Backend_Linux.ry, (uint)GfxCO);
            }
        }

        private void SetUpTempPower()
        {
            // Setup temp defaults for each power mode
            if (GetSystemInfo.Product.Contains("16"))
            {
                if (PowerIndex == 0)
                {
                    PL1 = 85;
                    PL2 = 85;
                    WinPower = 0;
                }
                else if (PowerIndex == 1)
                {
                    PL1 = 95;
                    PL2 = 95;
                    WinPower = 1;
                }
                else if (PowerIndex == 2)
                {
                    PL1 = 100;
                    PL2 = 120;
                    WinPower = 2;
                }

                PL1Min = 10;
                PL2Min = 10;
                PL1Max = 140;
                PL2Max = 140;
            }
        }
    }
}
