using Framework_Hub.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Hub.Services
{
    internal class AppSettings
    {
        public class Settings
        {
            public int lastPowerMode { get; set; } = 2;

        }

        internal class AppSettingsManager
        {
            private Dictionary<string, Settings> _settings;

            private readonly string _configDirectory;

            // set up manager instance
            public AppSettingsManager(string configDirectory)
            {
                _configDirectory = configDirectory;
                _settings = new Dictionary<string, Settings>();
                LoadPresets();
            }

            // Get data from preset
            public Settings GetPreset()
            {
                if (_settings.ContainsKey("Main Settings"))
                {
                    return _settings["Main Settings"];
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
                    _settings = JsonConvert.DeserializeObject<Dictionary<string, Settings>>(json);
                }
                else
                {
                    _settings = new Dictionary<string, Settings>();
                }
            }

            // Save preset to json file
            public void SaveSettings(Settings _newSettings)
            {
                _settings["Main Settings"] = _newSettings;
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
