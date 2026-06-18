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
                string query = "SELECT id_user, username, password, nama, id_armada FROM user WHERE id_role = 2 AND is_active = 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idArmadaDb = Convert.ToInt32(reader["id_armada"]);
                        Armada armada = (Armada)(idArmadaDb - 1);

                        listSopir.Add(new Sopir(
                            Convert.ToInt32(reader["id_user"]),
                            reader["nama"].ToString(),
                            reader["username"].ToString(),
                            reader["password"].ToString(),
                            armada
                        ));
                    }
                }
            }
            return listSopir;
        }
        public static void GenerateJadwalSopir(string username, Armada armadaTugas, DateTime tanggalPilih, int idUserDb)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string tglFormat = tanggalPilih.ToString("yyyy-MM-dd");

                string insertQuery = @"
            INSERT IGNORE INTO jadwal_pengiriman (tanggal_pengiriman, jumlah_pesanan, is_prioritas, id_pelanggan, id_user)
            SELECT 
                @tgl, 
                IF(p.galon_dipinjam > 0, p.galon_dipinjam, 1),
                IF(p.tanggal_terakhir_kirim <= @tgl - INTERVAL 8 DAY, 1, 0),
                p.id_pelanggan, 
                @idUser
            FROM pelanggan p
            JOIN user u ON p.id_armada = u.id_armada
            WHERE u.username = @username
            AND p.is_active = 1 
            AND u.is_active = 1
            AND (p.tanggal_terakhir_kirim IS NULL OR p.tanggal_terakhir_kirim <= @tgl - INTERVAL 7 DAY)";

                using (MySqlCommand cmdInsert = new MySqlCommand(insertQuery, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@tgl", tglFormat);
                    cmdInsert.Parameters.AddWithValue("@idUser", idUserDb);
                    cmdInsert.Parameters.AddWithValue("@username", username);
                    cmdInsert.ExecuteNonQuery();
                }
            }
        }
    }
}