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
                string query = "SELECT id_pelanggan, nama_pelanggan, alamat, id_armada FROM pelanggan";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idDb = Convert.ToInt32(reader["id_pelanggan"]);
                        string formatId = "P" + idDb.ToString("D3");

                        int idArmadaDb = reader["id_armada"] != DBNull.Value ? Convert.ToInt32(reader["id_armada"]) : 1;
                        Armada armada = (Armada)(idArmadaDb - 1);

                        DateTime tglTerakhir = DateTime.Today.AddDays(-7);

                        listPelanggan.Add(new Pelanggan(
                            formatId,
                            reader["nama_pelanggan"].ToString(),
                            reader["alamat"].ToString(),
                            armada,
                            tglTerakhir
                        ));
                    }
                }
            }
            return listPelanggan;
        }
    }
}