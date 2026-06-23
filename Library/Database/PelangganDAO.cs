using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HYMAPSOPIR;

namespace Library.Database
{
    public static class PelangganDAO
    {
        public static List<Pelanggan> GetAllPelanggan()
        {
            List<Pelanggan> listPelanggan = new List<Pelanggan>();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_pelanggan, nama_pelanggan, alamat, id_wilayah, tanggal_terakhir_kirim, galon_dipinjam FROM pelanggan WHERE is_active = 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idDb = Convert.ToInt32(reader["id_pelanggan"]);
                        string formatId = "P" + idDb.ToString("D3");

                        int idWilayahDb = reader["id_wilayah"] != DBNull.Value ? Convert.ToInt32(reader["id_wilayah"]) : 1;
                        Wilayah wilayah = Enum.IsDefined(typeof(Wilayah), idWilayahDb - 1) ? (Wilayah)(idWilayahDb - 1) : Wilayah.Denpasar;

                        DateTime tglTerakhir;
                        if (reader["tanggal_terakhir_kirim"] != DBNull.Value)
                        {
                            tglTerakhir = Convert.ToDateTime(reader["tanggal_terakhir_kirim"]);
                        }
                        else
                        {
                            
                            tglTerakhir = DateTime.Today.AddDays(-7);
                        }
                        int galonPinjamDb = reader["galon_dipinjam"] != DBNull.Value ? Convert.ToInt32(reader["galon_dipinjam"]) : 0;

                        listPelanggan.Add(new Pelanggan(
                            formatId,
                            reader["nama_pelanggan"].ToString(),
                            reader["alamat"].ToString(),
                            wilayah,
                            tglTerakhir,
                            galonPinjamDb
                        ));
                    }
                }
            }
            return listPelanggan;
        }
    }
}