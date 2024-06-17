using Framework_Hub.Scripts.Windows.Misc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Hub.Services
{
    internal class PowerModeSettings
    {
        public class PowerModePresets
        {
            public int _powerIndex { get; set; } = 2;

            public int _pl1 { get; set; } = 999;
            public int _pl2 { get; set; } = 999;

            public int _pl1Max { get; set; } = 999;
            public int _pl2Max { get; set; } = 999;
            public int _pl1Min { get; set; } = 999;
            public int _pl2Min { get; set; } = 999;

            public int _temp { get; set; } = 100;

            public int _allCO { get; set; } = 0;
            public int _gfxCO { get; set; } = 0;

            public int _pboOffset { get; set; } = 1;

            public int _winPower { get; set; } = 2;

        }

        internal class PowerModeSettingsManager
        {
            private Dictionary<string, PowerModePresets> _settings;

            private readonly string _configDirectory;
            string _device = GetSystemInfo.Product;

            // set up manager instance
            public PowerModeSettingsManager(string configDirectory)
            {
                _configDirectory = configDirectory;
                _settings = new Dictionary<string, PowerModePresets>();
                LoadPresets();
            }

            // Get data from preset
            public PowerModePresets GetPreset(int _powerMode)
            {
                if (_settings.ContainsKey($"{_device}_{_powerMode}"))
                {
                    return _settings[$"{_device}_{_powerMode}"];
                }
                else
                {
                    return null;
                }
            }

            // Load all presents into string dictionary
            private void LoadPresets()
            {
                if (File.Exists(_configDirectory))
                {
                    string json = File.ReadAllText(_configDirectory);
                    _settings = JsonConvert.DeserializeObject<Dictionary<string, PowerModePresets>>(json);
                }
                else
                {
                    _settings = new Dictionary<string, PowerModePresets>();
                }
            }

            // Save preset to json file
            public void SaveSettings(PowerModePresets _newPreset, int _powerMode)
            {
                _settings[$"{_device}_{_powerMode}"] = _newPreset;
                SaveAppSettings();
            }

            // Save json file changes
            private void SaveAppSettings()
            {
                string json = JsonConvert.SerializeObject(_settings, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(_configDirectory, json);
            }
        }
    }
}