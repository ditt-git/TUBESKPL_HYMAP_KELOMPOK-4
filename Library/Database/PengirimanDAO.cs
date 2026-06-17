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

                // Ambil id_pengiriman dari jadwal_pengiriman
                string queryGetId = @"SELECT id_pengiriman FROM jadwal_pengiriman 
                                      WHERE id_pelanggan = @idPelanggan AND tanggal_pengiriman = @tanggalTugas
                                      ORDER BY id_pengiriman DESC LIMIT 1";

                int? idPengiriman = null;
                using (MySqlCommand cmd = new MySqlCommand(queryGetId, conn))
                {
                    cmd.Parameters.AddWithValue("@idPelanggan", idPelangganInt);
                    cmd.Parameters.AddWithValue("@tanggalTugas", tglFormat);
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        idPengiriman = Convert.ToInt32(result);
                    }
                }

                if (idPengiriman == null)
                {
                    // Jika jadwal pengiriman belum ada, buat jadwal baru secara otomatis
                    string queryInsertJadwal = @"INSERT INTO jadwal_pengiriman (tanggal_pengiriman, jumlah_pesanan, is_prioritas, id_pelanggan, id_user) 
                                                 VALUES (@tanggalTugas, @jumlahPesanan, 0, @idPelanggan, @idUser)";
                    using (MySqlCommand cmdInsert = new MySqlCommand(queryInsertJadwal, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@tanggalTugas", tglFormat);
                        cmdInsert.Parameters.AddWithValue("@jumlahPesanan", tugas.DataPelanggan.GalonDipinjam > 0 ? tugas.DataPelanggan.GalonDipinjam : 1);
                        cmdInsert.Parameters.AddWithValue("@idPelanggan", idPelangganInt);
                        cmdInsert.Parameters.AddWithValue("@idUser", (int)tugas.DataPelanggan.Wilayah + 1);
                        cmdInsert.ExecuteNonQuery();
                        idPengiriman = Convert.ToInt32(cmdInsert.LastInsertedId);
                    }
                }

                // Cek apakah laporan sudah ada untuk id_pengiriman ini
                string queryCek = @"SELECT id_laporan FROM laporan WHERE id_pengiriman = @idPengiriman ORDER BY id_laporan DESC LIMIT 1";
                int? existingLaporanId = null;
                using (MySqlCommand cmd = new MySqlCommand(queryCek, conn))
                {
                    cmd.Parameters.AddWithValue("@idPengiriman", idPengiriman.Value);
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        existingLaporanId = Convert.ToInt32(result);
                    }
                }

                if (existingLaporanId != null)
                {
                    // UPDATE laporan yang sudah ada
                    string queryUpdate = @"UPDATE laporan SET status_pengiriman = @statusKirim, status_pembayaran = @statusBayar, 
                                           bukti_foto = @buktiFoto, galon_kembali = @galonKembali 
                                           WHERE id_laporan = @idLaporan";
                    using (MySqlCommand cmd = new MySqlCommand(queryUpdate, conn))
                    {
                        cmd.Parameters.AddWithValue("@statusKirim", tugas.StatusKirim.ToString());
                        cmd.Parameters.AddWithValue("@statusBayar", tugas.StatusBayar.ToString());
                        cmd.Parameters.AddWithValue("@buktiFoto", string.IsNullOrEmpty(tugas.BuktiFoto) ? DBNull.Value : (object)tugas.BuktiFoto);
                        cmd.Parameters.AddWithValue("@galonKembali", tugas.GalonKembali);
                        cmd.Parameters.AddWithValue("@idLaporan", existingLaporanId.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // INSERT laporan baru
                    string queryInsert = @"INSERT INTO laporan (id_pengiriman, waktu_submit, status_pengiriman, status_pembayaran, bukti_foto, galon_kembali) 
                                           VALUES (@idPengiriman, @waktuSubmit, @statusKirim, @statusBayar, @buktiFoto, @galonKembali)";
                    using (MySqlCommand cmd = new MySqlCommand(queryInsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@idPengiriman", idPengiriman.Value);
                        cmd.Parameters.AddWithValue("@waktuSubmit", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@statusKirim", tugas.StatusKirim.ToString());
                        cmd.Parameters.AddWithValue("@statusBayar", tugas.StatusBayar.ToString());
                        cmd.Parameters.AddWithValue("@buktiFoto", string.IsNullOrEmpty(tugas.BuktiFoto) ? DBNull.Value : (object)tugas.BuktiFoto);
                        cmd.Parameters.AddWithValue("@galonKembali", tugas.GalonKembali);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Menyimpan tanggal terakhir kirim ke database agar persisten setelah logout
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

        public static bool CekStatusLaporan(string idPelangganFormatted, DateTime tanggalTugas, out StatusPengiriman statusKirim, out StatusPembayaran statusBayar, out string buktiFoto)
        {
            statusKirim = StatusPengiriman.BelumTerkirim;
            statusBayar = StatusPembayaran.Bon;
            buktiFoto = "";

            int idPelangganInt = int.Parse(idPelangganFormatted.Substring(1));

            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                // Mengambil laporan terbaru untuk pelanggan ini pada hari ini
                string query = @"SELECT l.status_pengiriman, l.status_pembayaran, l.bukti_foto 
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
                            buktiFoto = reader["bukti_foto"] != DBNull.Value ? reader["bukti_foto"].ToString() : "";
                            return true;
                        }
                    }
                }
            }
            return false;
        }


    }
}
