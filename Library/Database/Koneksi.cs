using System;
using MySql.Data.MySqlClient;

namespace Library.Database
{
    public class Koneksi
    {
        private static Koneksi _instance;
        private readonly string _connectionString = "Server=localhost;Database=hymap;Uid=root;Pwd=;";

        private Koneksi() { }

        // KISS
        public static Koneksi Instance => _instance ??= new Koneksi();
        public MySqlConnection GetConnection() => new MySqlConnection(_connectionString);
    }
}