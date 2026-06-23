using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HYMAPSOPIR;

namespace Library.Database
{
    public static class SopirDAO
    {
        public static List<Sopir> GetAllSopir()
        {
            List<Sopir> listSopir = new List<Sopir>();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                // Mengambil user dengan id_role = 2 (Sopir)
                string query = "SELECT id_user, username, password, nama, id_wilayah FROM user WHERE id_role = 2 AND is_active = 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idWilayahDb = reader["id_wilayah"] != DBNull.Value ? Convert.ToInt32(reader["id_wilayah"]) : 0;
                        Wilayah wilayah = Enum.IsDefined(typeof(Wilayah), idWilayahDb - 1) ? (Wilayah)(idWilayahDb - 1) : Wilayah.Denpasar;

                        listSopir.Add(new Sopir(
                            Convert.ToInt32(reader["id_user"]),
                            reader["nama"].ToString(),
                            reader["username"].ToString(),
                            reader["password"].ToString(),
                            wilayah
                        ));
                    }
                }
            }
            return listSopir;
        }
        public static void GenerateJadwalSopir(string username, Wilayah wilayahTugas, DateTime tanggalPilih, int idUserDb)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string tglFormat = tanggalPilih.ToString("yyyy-MM-dd");

                string insertQuery = @"
            INSERT IGNORE INTO pengiriman (tanggal_pengiriman, jumlah_pesanan, is_prioritas, id_pelanggan, id_user)
            SELECT 
                @tgl, 
                IF(p.galon_dipinjam > 0, p.galon_dipinjam, 1),
                IF(p.tanggal_terakhir_kirim IS NULL OR DATEDIFF(@tgl, p.tanggal_terakhir_kirim) >= 8, 1, 0),
                p.id_pelanggan, 
                @idUser
            FROM pelanggan p
            JOIN user u ON p.id_wilayah = u.id_wilayah
            WHERE u.username = @username
            AND p.is_active = 1 
            AND u.is_active = 1
            AND (p.tanggal_terakhir_kirim IS NULL OR DATEDIFF(@tgl, p.tanggal_terakhir_kirim) >= 7)";

                using (MySqlCommand cmdInsert = new MySqlCommand(insertQuery, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@tgl", tglFormat);
                    cmdInsert.Parameters.AddWithValue("@idUser", idUserDb);
                    cmdInsert.Parameters.AddWithValue("@username", username);
                    cmdInsert.ExecuteNonQuery();
                }
            }
        }

        public static List<string> GetJadwalPelangganSopir(int idUserDb, DateTime tanggal)
        {
            List<string> listIdPelanggan = new List<string>();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "SELECT p.id_pelanggan FROM pengiriman p JOIN pelanggan pel ON p.id_pelanggan = pel.id_pelanggan WHERE p.id_user = @idUser AND p.tanggal_pengiriman = @tgl AND pel.is_active = 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idUser", idUserDb);
                    cmd.Parameters.AddWithValue("@tgl", tanggal.ToString("yyyy-MM-dd"));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idDb = Convert.ToInt32(reader["id_pelanggan"]);
                            string idFormatted = "P" + idDb.ToString("D3");
                            listIdPelanggan.Add(idFormatted);
                        }
                    }
                }
            }
            return listIdPelanggan;
        }
    }
}