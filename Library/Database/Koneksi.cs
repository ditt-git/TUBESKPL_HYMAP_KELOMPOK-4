using System;
using System.Data;
using MySql.Data.MySqlClient;
using HYMAPSOPIR;

namespace Library.Database
{
    public class Koneksi 
    {
        // Variabel private untuk menyimpan satu-satunya instance
        private static Koneksi _instance;

        private string connectionString = "Server=localhost;Database=hymap;Uid=root;Pwd=;";

        // Private Constructor agar tidak bisa di-new() sembarangan
        private Koneksi()
        {
        }

        // Global Point of Access/Inti dari Singleton
        public static Koneksi Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Koneksi();
                }
                return _instance;
            }
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}