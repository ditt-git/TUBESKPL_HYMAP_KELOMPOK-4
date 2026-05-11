using System;
using System.IO;
using System.Text.Json;

namespace HYMAPSOPIR
{
    public static class ConfigManager
    {
        private const string ConfigFileName = "config.json";
        private static AppConfig _config;

        public static AppConfig Instance
        {
            get
            {
                if (_config == null)
                {
                    Load();
                }
                return _config;
            }
        }

        public static void Load()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
                
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    _config = JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
                }
                else
                {
                    _config = new AppConfig();
                }
            }
            catch (Exception ex)
            {
                // Fallback to default if loading fails
                _config = new AppConfig();
                Console.WriteLine($"Failed to load config: {ex.Message}");
            }
        }
    }
}
