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

                string query = @"INSERT INTO jadwal_pengiriman 
                                (tanggal_pengiriman, jumlah_pesanan, status_pengiriman, status_pembayaran, is_prioritas, total_harga, id_pelanggan) 
                                VALUES (@tgl, @jumlah, @statusKirim, @statusBayar, @isPrioritas, @totalHarga, @idPelanggan)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tgl", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@jumlah", 1); // Default 1 galon sementara
                    cmd.Parameters.AddWithValue("@statusKirim", tugas.StatusKirim.ToString());
                    cmd.Parameters.AddWithValue("@statusBayar", tugas.StatusBayar.ToString());
                    cmd.Parameters.AddWithValue("@isPrioritas", tugas.Prioritas != PrioritasPengiriman.Normal ? 1 : 0);
                    cmd.Parameters.AddWithValue("@totalHarga", 15000); // Misal harga default
                    cmd.Parameters.AddWithValue("@idPelanggan", idPelangganInt);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}