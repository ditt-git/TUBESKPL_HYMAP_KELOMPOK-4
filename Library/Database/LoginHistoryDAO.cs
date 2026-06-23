using System;
using MySql.Data.MySqlClient;

namespace Library.Database
{
    public class LoginHistoryDAO
    {
        private static LoginHistoryDAO _instance;

        private LoginHistoryDAO()
        {
        }
        public static LoginHistoryDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoginHistoryDAO();
                }
                return _instance;
            }
        }

        public int CatatLogin(string username)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO history_login (id_user, waktu_login) 
                                 SELECT id_user, NOW() 
                                 FROM user 
                                 WHERE username = @username AND is_active = 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(cmd.LastInsertedId);
                }
            }
        }

        public void CatatLogout(int idLog)
        {
            if (idLog <= 0) return;

            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "UPDATE history_login SET waktu_logout = NOW() WHERE id_log = @idLog";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idLog", idLog);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}