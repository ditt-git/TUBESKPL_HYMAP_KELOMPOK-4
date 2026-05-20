using System;
using System.IO;
using System.Windows.Forms;

namespace HYMAPSOPIR
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            appConfig.Load();

            if (appConfig.IsMaintenanceMode)
            {
                MessageBox.Show(
                    appConfig.MaintenanceMessage,
                    "HYMAP Sistem Maintenance",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return; 
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new FormLogin());
        }
    }
}