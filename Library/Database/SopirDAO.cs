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
    }
}