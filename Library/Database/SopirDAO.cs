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
                string query = "SELECT username, password, nama, id_armada FROM user WHERE id_role = 2";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idArmadaDb = Convert.ToInt32(reader["id_armada"]);
                        Armada armada = (Armada)(idArmadaDb - 1);

                        listSopir.Add(new Sopir(
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
        public static void GenerateJadwalSopir(string username, Armada armadaTugas, DateTime tanggalPilih)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                int idUserTugas = (int)armadaTugas + 1;
                string tglFormat = tanggalPilih.ToString("yyyy-MM-dd");

                // Cek apakah jadwal untuk tanggal yang DIPILIH sudah ada di database
                string cekQuery = "SELECT COUNT(*) FROM jadwal_pengiriman WHERE tanggal_pengiriman = @tgl AND id_user = @idUser";
                using (MySqlCommand cmd = new MySqlCommand(cekQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@tgl", tglFormat);
                    cmd.Parameters.AddWithValue("@idUser", idUserTugas);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 0)
                    {
                        // Jika belum ada, buat jadwal baru untuk tanggal tersebut
                        string insertQuery = @"
            INSERT INTO jadwal_pengiriman (tanggal_pengiriman, jumlah_pesanan, is_prioritas, id_pelanggan, id_user)
            SELECT 
                @tgl, 
                IF(p.galon_dipinjam > 0, p.galon_dipinjam, 1),
                IF(p.tanggal_terakhir_kirim <= @tgl - INTERVAL 8 DAY, 1, 0),
                p.id_pelanggan, 
                u.id_user
            FROM pelanggan p
            JOIN user u ON p.id_armada = u.id_armada
            WHERE u.username = @username
            AND (p.tanggal_terakhir_kirim IS NULL OR p.tanggal_terakhir_kirim <= @tgl - INTERVAL 7 DAY)";

                        using (MySqlCommand cmdInsert = new MySqlCommand(insertQuery, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@tgl", tglFormat);
                            cmdInsert.Parameters.AddWithValue("@username", username);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}