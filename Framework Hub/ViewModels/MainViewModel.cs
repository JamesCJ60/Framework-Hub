﻿using Avalonia;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;
using Framework_Hub.Scripts.Windows.RyzenAdj;
using Framework_Hub.Scripts.Linux.RyzenAdj;
using System.Runtime.InteropServices;
using Framework_Hub.Scripts.Windows.Misc;
using System.Linq.Expressions;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Framework_Hub.Services;
using static Framework_Hub.Services.AppSettings;
using System.IO;
using static Framework_Hub.Services.PowerModeSettings;

namespace Framework_Hub.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private int _powerIndex = 2;

        private int _pl1 = 999;
        private int _pl2 = 999;

        private int _pl1Max = 999;
        private int _pl2Max = 999;
        private int _pl1Min = 999;
        private int _pl2Min = 999;

        private int _temp = 100;

        private int _allCO = 0;
        private int _gfxCO = 0;

        private int _pboOffset = 1;

        private int _winPower;
        private string _winPowerText;

        public event System.EventHandler PowerUpdated;

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

        AppSettingsManager appSettings = new AppSettingsManager("Settings.json");
        PowerModeSettingsManager powerModeSettings = new PowerModeSettingsManager("PowerSettings.json");

        public MainViewModel()
        {
            if (File.Exists("Settings.json"))
            {
                Settings settings = appSettings.GetPreset();
                PowerIndex = settings.lastPowerMode;
            }
            else PowerIndex = 2;


            // Setup event to detect variable changes
            var propertySelectors = new Expression<Func<MainViewModel, int>>[]
            {
            x => x.Temp,
            x => x.PL1,
            x => x.PL2,
            x => x.AllCO,
            x => x.GfxCO,
            x => x.PboOffset,
            x => x.WinPower,
            x => x.PowerIndex
            };

            var observables = propertySelectors
                .Select(selector => this.WhenAnyValue<MainViewModel, int>(selector))
                .ToArray();

            var mergedObservable = Observable.Merge(observables);

            mergedObservable.Subscribe(_ => OnPowerChange());

            // Setup temp defaults for each power mode
            SetUpTempPower();
        }

        int lastPowerMode = -1;
        private async void OnPowerChange()
        {
            // Update Windows power mode setting 
            WinPowerMode.SetWinPowerMode(WinPower);

            // Update Windows power mode icon
            if (WinPower == 0) WinPowerText = "\ue8be";
            else if (WinPower == 1) WinPowerText = "\uec49";
            else if (WinPower == 2) WinPowerText = "\uec4a";

            if (lastPowerMode != PowerIndex)
            {
                SetUpTempPower();
                lastPowerMode = PowerIndex;
            }

            SavePreset();

            await ApplyPowerSettings();
        }

        public async Task ApplyPowerSettings()
        {
            await Task.Delay(1500); // Delay for 1.5 seconds

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
                if (AllCO < 0) RyzenAdj_Backend_Windows.set_coall(RyzenAdj_Backend_Windows.ry, Convert.ToUInt32(0x100000 - (uint)(-1 * AllCO)));
                else RyzenAdj_Backend_Windows.set_coall(RyzenAdj_Backend_Windows.ry, 0);

                // Set iGPU Curve Optimiser offset
                if (GfxCO < 0) RyzenAdj_Backend_Windows.set_cogfx(RyzenAdj_Backend_Windows.ry, Convert.ToUInt32(0x100000 - (uint)(-1 * GfxCO)));
                else RyzenAdj_Backend_Windows.set_cogfx(RyzenAdj_Backend_Windows.ry, 0);

                // PBO Scalar Offset
                RyzenAdj_Backend_Windows.pbo_scalar(RyzenAdj_Backend_Windows.ry, (uint)(PboOffset * 100));
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
                if (AllCO < 0) RyzenAdj_Backend_Linux.set_coall(RyzenAdj_Backend_Linux.ry, Convert.ToUInt32(0x100000 - (uint)(-1 * AllCO)));
                else RyzenAdj_Backend_Linux.set_coall(RyzenAdj_Backend_Linux.ry, 0);

                // Set iGPU Curve Optimiser offset
                if (GfxCO < 0) RyzenAdj_Backend_Linux.set_cogfx(RyzenAdj_Backend_Linux.ry, Convert.ToUInt32(0x100000 - (uint)(-1 * GfxCO)));
                else RyzenAdj_Backend_Linux.set_cogfx(RyzenAdj_Backend_Linux.ry, 0);

                // PBO Scalar Offset
                RyzenAdj_Backend_Linux.pbo_scalar(RyzenAdj_Backend_Linux.ry, (uint)(PboOffset * 100));
            }
        }

        private void SetUpTempPower()
        {
            if (powerModeSettings.GetPreset(PowerIndex) != null && File.Exists("PowerSettings.json"))
            {
                PowerModePresets _powerPreset = powerModeSettings.GetPreset(PowerIndex);
                Temp = _powerPreset._temp;
                PL1 = _powerPreset._pl1;
                PL2 = _powerPreset._pl2;
                PL1Max = _powerPreset._pl1Max;
                PL2Max = _powerPreset._pl2Max;
                PL1Min = _powerPreset._pl1Min;
                PL2Min = _powerPreset._pl2Min;
                AllCO = _powerPreset._allCO;
                GfxCO = _powerPreset._gfxCO;
                PboOffset = _powerPreset._pboOffset;
                WinPower = _powerPreset._winPower;
            }
            else
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
                else if (GetSystemInfo.Product.Contains("13"))
                {
                    if (PowerIndex == 0)
                    {
                        PL1 = 15;
                        PL2 = 18;
                        WinPower = 0;
                    }
                    else if (PowerIndex == 1)
                    {
                        PL1 = 28;
                        PL2 = 28;
                        WinPower = 1;
                    }
                    else if (PowerIndex == 2)
                    {
                        PL1 = 35;
                        PL2 = 60;
                        WinPower = 2;
                    }

                    PL1Min = 5;
                    PL2Min = 5;
                    PL1Max = 60;
                    PL2Max = 60;

                    SavePreset();
                }
            }

            Settings _settings = new Settings()
            {
                lastPowerMode = PowerIndex,
            };
            appSettings.SaveSettings(_settings);
        }

        private void SavePreset()
        {
            PowerModePresets _powerMode = new PowerModePresets()
            {
                _temp = Temp,
                _pl1Max = PL1Max,
                _pl2Max = PL2Max,
                _pl1Min = PL1Min,
                _pl2Min = PL2Min,
                _pl1 = PL1,
                _pl2 = PL2,
                _winPower = WinPower,
                _allCO = AllCO,
                _gfxCO = GfxCO,
                _pboOffset = PboOffset,
            };
            powerModeSettings.SaveSettings(_powerMode, PowerIndex);
        }
    }
}
