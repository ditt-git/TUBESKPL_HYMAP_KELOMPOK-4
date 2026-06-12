using System;
using System.IO;
using System.Text.Json;

namespace HYMAPSOPIR
{
    public static class appConfig
    {
        public static bool IsMaintenanceMode { get; set; } = true;
        public static string MaintenanceMessage { get; set; } = "Sistem sedang maintenance!";

        private static string configPath = "config.json";

        private class ConfigModel
        {
            public bool IsMaintenanceMode { get; set; }
            public string MaintenanceMessage { get; set; }
        }

        public static void Load()
        {
            if (!File.Exists(configPath))
            {
                return;
            }

            try
            {
                string jsonString = File.ReadAllText(configPath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                ConfigModel config = JsonSerializer.Deserialize<ConfigModel>(jsonString, options);

                if (config != null)
                {
                    IsMaintenanceMode = config.IsMaintenanceMode;
                    MaintenanceMessage = config.MaintenanceMessage;
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("Format file config.json tidak valid.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal membaca config: {ex.Message}");
            }
        }
    }
}