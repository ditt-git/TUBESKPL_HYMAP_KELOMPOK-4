using System;
using MySql.Data.MySqlClient;
using HYMAPSOPIR;

namespace Library.Database
{
    public static class PengirimanDAO
    {
        public static void SimpanJadwalPengiriman(Pengiriman tugas)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();

                int idPelangganInt = int.Parse(tugas.DataPelanggan.IdPelanggan.Substring(1));
                string tglFormat = tugas.TanggalTugas.ToString("yyyy-MM-dd");

                // (Mencegah Jadwal Kembar akibat Double-Click)
                
                string queryInsertJadwal = @"
                    INSERT IGNORE INTO jadwal_pengiriman (tanggal_pengiriman, jumlah_pesanan, is_prioritas, id_pelanggan, id_user) 
                    VALUES (@tanggalTugas, @jumlahPesanan, 0, @idPelanggan, @idUser)";
                    
                using (MySqlCommand cmdInsert = new MySqlCommand(queryInsertJadwal, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@tanggalTugas", tglFormat);
                    cmdInsert.Parameters.AddWithValue("@jumlahPesanan", tugas.DataPelanggan.GalonDipinjam > 0 ? tugas.DataPelanggan.GalonDipinjam : 1);
                    cmdInsert.Parameters.AddWithValue("@idPelanggan", idPelangganInt);
                    cmdInsert.Parameters.AddWithValue("@idUser", tugas.IdUserSopir);
                    cmdInsert.ExecuteNonQuery();
                }

                // dapat 1 ID saja
                string queryGetId = @"SELECT id_pengiriman FROM jadwal_pengiriman 
                                      WHERE id_pelanggan = @idPelanggan AND tanggal_pengiriman = @tanggalTugas
                                      ORDER BY id_pengiriman DESC LIMIT 1";
                                      
                int idPengiriman;
                using (MySqlCommand cmd = new MySqlCommand(queryGetId, conn))
                {
                    cmd.Parameters.AddWithValue("@idPelanggan", idPelangganInt);
                    cmd.Parameters.AddWithValue("@tanggalTugas", tglFormat);
                    idPengiriman = Convert.ToInt32(cmd.ExecuteScalar());
                }

            
                // Menggunakan ON DUPLICATE KEY UPDATE untuk memastikan tidak ada 2 Laporan
                string queryUpsert = @"
                    INSERT INTO laporan (id_pengiriman, waktu_submit, status_pengiriman, status_pembayaran, galon_kembali) 
                    VALUES (@idPengiriman, @waktuSubmit, @statusKirim, @statusBayar, @galonKembali)
                    ON DUPLICATE KEY UPDATE 
                        status_pengiriman = @statusKirim,
                        status_pembayaran = @statusBayar,
                        galon_kembali = @galonKembali,
                        waktu_submit = @waktuSubmit";

                using (MySqlCommand cmd = new MySqlCommand(queryUpsert, conn))
                {
                    cmd.Parameters.AddWithValue("@idPengiriman", idPengiriman);
                    cmd.Parameters.AddWithValue("@waktuSubmit", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@statusKirim", tugas.StatusKirim.ToString());
                    cmd.Parameters.AddWithValue("@statusBayar", tugas.StatusBayar.ToString());
                    cmd.Parameters.AddWithValue("@galonKembali", tugas.GalonKembali);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateTanggalTerakhirKirim(string idPelangganFormatted, DateTime tanggalKirim)
        {
            int idPelangganInt = int.Parse(idPelangganFormatted.Substring(1));

            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE pelanggan SET tanggal_terakhir_kirim = @tanggalKirim WHERE id_pelanggan = @idPelanggan";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tanggalKirim", tanggalKirim.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@idPelanggan", idPelangganInt);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool CekStatusLaporan(string idPelangganFormatted, DateTime tanggalTugas, out StatusPengiriman statusKirim, out StatusPembayaran statusBayar)
        {
            statusKirim = StatusPengiriman.BelumTerkirim;
            statusBayar = StatusPembayaran.Bon;


            int idPelangganInt = int.Parse(idPelangganFormatted.Substring(1));

            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                // Mengambil laporan terbaru untuk pelanggan ini pada hari ini
                string query = @"SELECT l.status_pengiriman, l.status_pembayaran 
                         FROM laporan l
                         JOIN jadwal_pengiriman j ON l.id_pengiriman = j.id_pengiriman
                         WHERE j.id_pelanggan = @idPelanggan 
                         AND j.tanggal_pengiriman = @tanggalTugas
                         ORDER BY l.id_laporan DESC LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPelanggan", idPelangganInt);
                    cmd.Parameters.AddWithValue("@tanggalTugas", tanggalTugas.ToString("yyyy-MM-dd"));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Enum.TryParse(reader["status_pengiriman"].ToString(), out statusKirim);
                            Enum.TryParse(reader["status_pembayaran"].ToString(), out statusBayar);

                            return true;
                        }
                    }
                }
            }
            return false;
        }


    }
}
