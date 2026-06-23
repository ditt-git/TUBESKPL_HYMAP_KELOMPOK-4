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
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int idPelangganInt = int.Parse(tugas.DataPelanggan.IdPelanggan.Substring(1));
                        string tglFormat = tugas.TanggalTugas.ToString("yyyy-MM-dd");

                        if (tugas.StatusKirim == StatusPengiriman.SudahTerkirim)
                        {
                            string queryHapus = @"DELETE FROM pengiriman 
                                                 WHERE id_pelanggan = @idPelanggan 
                                                 AND tanggal_pengiriman > @tanggalKirim 
                                                 AND (status_pengiriman = 'BelumTerkirim' OR status_pengiriman IS NULL)";
                            using (MySqlCommand cmdHapus = new MySqlCommand(queryHapus, conn, transaction))
                            {
                                cmdHapus.Parameters.AddWithValue("@idPelanggan", idPelangganInt);
                                cmdHapus.Parameters.AddWithValue("@tanggalKirim", tglFormat);
                                cmdHapus.ExecuteNonQuery();
                            }
                        }

                        string queryUpsert = @"
                            INSERT INTO pengiriman (tanggal_pengiriman, jumlah_pesanan, is_prioritas, id_pelanggan, id_user, status_pengiriman, status_pembayaran, galon_kembali, waktu_submit) 
                            VALUES (@tanggalTugas, @jumlahPesanan, 0, @idPelanggan, @idUser, @statusKirim, @statusBayar, @galonKembali, @waktuSubmit)
                            ON DUPLICATE KEY UPDATE 
                                status_pengiriman = @statusKirim,
                                status_pembayaran = @statusBayar,
                                galon_kembali = @galonKembali,
                                waktu_submit = @waktuSubmit";
                            
                        using (MySqlCommand cmdInsert = new MySqlCommand(queryUpsert, conn, transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@tanggalTugas", tglFormat);
                            cmdInsert.Parameters.AddWithValue("@jumlahPesanan", tugas.JumlahPesanan);
                            cmdInsert.Parameters.AddWithValue("@idPelanggan", idPelangganInt);
                            cmdInsert.Parameters.AddWithValue("@idUser", tugas.IdUserSopir);
                            cmdInsert.Parameters.AddWithValue("@waktuSubmit", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            cmdInsert.Parameters.AddWithValue("@statusKirim", tugas.StatusKirim.ToString());
                            cmdInsert.Parameters.AddWithValue("@statusBayar", tugas.StatusBayar.ToString());
                            cmdInsert.Parameters.AddWithValue("@galonKembali", tugas.GalonKembali);

                            cmdInsert.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
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



        public static bool CekStatusLaporan(string idPelangganFormatted, DateTime tanggalTugas, out StatusPengiriman statusKirim, out StatusPembayaran statusBayar, out int jumlahPesanan, out int galonKembali)
        {
            statusKirim = StatusPengiriman.BelumTerkirim;
            statusBayar = StatusPembayaran.Bon;
            jumlahPesanan = 0;
            galonKembali = 0;

            int idPelangganInt = int.Parse(idPelangganFormatted.Substring(1));

            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                // Mengambil laporan terbaru untuk pelanggan ini pada hari ini
                string query = @"SELECT status_pengiriman, status_pembayaran, jumlah_pesanan, galon_kembali 
                         FROM pengiriman
                         WHERE id_pelanggan = @idPelanggan 
                         AND tanggal_pengiriman = @tanggalTugas
                         ORDER BY id_pengiriman DESC LIMIT 1";

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
                            jumlahPesanan = Convert.ToInt32(reader["jumlah_pesanan"]);
                            galonKembali = reader["galon_kembali"] != DBNull.Value ? Convert.ToInt32(reader["galon_kembali"]) : 0;

                            return true;
                        }
                    }
                }
            }
            return false;
        }


    }
}
