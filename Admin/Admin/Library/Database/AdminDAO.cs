using System;
using System.Data;
using MySql.Data.MySqlClient;
using Admin.Library.Security;

namespace Admin.Library.Database
{
    public static class AdminDAO
    {
        // ================= 1. FITUR LOGIN =================
        public static bool LoginAdmin(string username, string password)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "SELECT password FROM user WHERE username = @user AND id_role = 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    var dbPassword = cmd.ExecuteScalar()?.ToString();

                    if (dbPassword != null && dbPassword == SecurityHelper.HashSHA256(password))
                        return true;
                    return false;
                }
            }
        }

        // ================= 2. FITUR KELOLA SOPIR =================
        public static DataTable GetAllSopir()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                // REVISI: Menghapus "AND is_active = 1"
                string query = "SELECT id_user, nama, no_telepon, username, id_armada FROM user WHERE id_role = 2";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static void TambahSopir(string nama, string noTelp, string username, string password, int idArmada)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                // REVISI: Menghapus kolom is_active dari INSERT
                string query = "INSERT INTO user (nama, no_telepon, username, password, id_role, id_armada) VALUES (@nama, @noTelp, @user, @pass, 2, @idArmada)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@noTelp", string.IsNullOrEmpty(noTelp) ? DBNull.Value : (object)noTelp);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", SecurityHelper.HashSHA256(password));
                    cmd.Parameters.AddWithValue("@idArmada", idArmada);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void EditSopir(int idUser, string nama, string noTelp, string username, int idArmada)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "UPDATE user SET nama=@nama, no_telepon=@noTelp, username=@user, id_armada=@idArmada WHERE id_user=@id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@noTelp", string.IsNullOrEmpty(noTelp) ? DBNull.Value : (object)noTelp);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@idArmada", idArmada);
                    cmd.Parameters.AddWithValue("@id", idUser);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ================= 3. FITUR KELOLA PELANGGAN =================
        public static DataTable GetAllPelanggan()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                // REVISI: Menghapus "WHERE is_active = 1"
                string query = "SELECT id_pelanggan, nama_pelanggan, alamat, no_telepon, id_armada, harga_default FROM pelanggan";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // ================= 4. FITUR KELOLA ARMADA =================
        public static DataTable GetAllArmada()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_armada, nama_wilayah, harga_pengiriman FROM armada";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static void EditArmada(int idArmada, string namaWilayah, decimal hargaPengiriman)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "UPDATE armada SET nama_wilayah = @nama, harga_pengiriman = @harga WHERE id_armada = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", namaWilayah);
                    cmd.Parameters.AddWithValue("@harga", hargaPengiriman);
                    cmd.Parameters.AddWithValue("@id", idArmada);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}