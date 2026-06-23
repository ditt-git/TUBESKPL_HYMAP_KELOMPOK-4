using System;
using System.Data;
using MySql.Data.MySqlClient;
using Admin.Library.Security;

namespace Admin.Library.Database
{
    public static class AdminDAO
    {
        // ================= 1. FITUR LOGIN =================
        public static bool LoginAdmin(string username, string hashedPassword)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "SELECT password FROM user WHERE username = @user AND id_role = 1 AND is_active = 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    var dbPassword = cmd.ExecuteScalar()?.ToString();

                    if (dbPassword != null && dbPassword == hashedPassword)
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
                string query = "SELECT id_user, nama, no_telepon, username, id_wilayah, is_active FROM user WHERE id_role = 2";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static bool TambahSopir(string nama, string noTelp, string username, string password, int idArmada)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                
                int idWilayah;
                using (MySqlCommand cmdWilayah = new MySqlCommand("SELECT id_wilayah FROM armada WHERE id_armada = @idArmada", conn))
                {
                    cmdWilayah.Parameters.AddWithValue("@idArmada", idArmada);
                    var res = cmdWilayah.ExecuteScalar();
                    if (res == null) throw new Exception("Armada tidak ditemukan.");
                    idWilayah = Convert.ToInt32(res);
                }

                string query = "INSERT INTO user (nama, no_telepon, username, password, id_role, id_wilayah, id_armada_kendaraan) VALUES (@nama, @noTelp, @user, @pass, 2, @idWilayah, @idArmada)";
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@noTelp", string.IsNullOrEmpty(noTelp) ? DBNull.Value : (object)noTelp);
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", SecurityHelper.HashSHA256(password));
                        cmd.Parameters.AddWithValue("@idWilayah", idWilayah);
                        cmd.Parameters.AddWithValue("@idArmada", idArmada);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (MySqlException ex) when (ex.Number == 1062)
                {
                    throw new Exception("Gagal: Username tersebut sudah digunakan.");
                }
            }
        }

        public static bool EditSopir(int idUser, string nama, string noTelp, string username, int idArmada, string password)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();

                int idWilayah;
                using (MySqlCommand cmdWilayah = new MySqlCommand("SELECT id_wilayah FROM armada WHERE id_armada = @idArmada", conn))
                {
                    cmdWilayah.Parameters.AddWithValue("@idArmada", idArmada);
                    var res = cmdWilayah.ExecuteScalar();
                    if (res == null) throw new Exception("Armada tidak ditemukan.");
                    idWilayah = Convert.ToInt32(res);
                }

                string query = "UPDATE user SET nama=@nama, no_telepon=@noTelp, username=@user, id_wilayah=@idWilayah, id_armada_kendaraan=@idArmada";
                if (!string.IsNullOrEmpty(password))
                {
                    query += ", password=@pass";
                }
                query += " WHERE id_user=@id AND id_role=2";
                
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@noTelp", string.IsNullOrEmpty(noTelp) ? DBNull.Value : (object)noTelp);
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@idWilayah", idWilayah);
                        cmd.Parameters.AddWithValue("@idArmada", idArmada);
                        if (!string.IsNullOrEmpty(password))
                        {
                            cmd.Parameters.AddWithValue("@pass", SecurityHelper.HashSHA256(password));
                        }
                        cmd.Parameters.AddWithValue("@id", idUser);
                        
                        int affected = cmd.ExecuteNonQuery();
                        if (affected == 0)
                        {
                            using (MySqlCommand checkExist = new MySqlCommand("SELECT COUNT(*) FROM user WHERE id_user = @id AND id_role = 2", conn))
                            {
                                checkExist.Parameters.AddWithValue("@id", idUser);
                                if (Convert.ToInt32(checkExist.ExecuteScalar()) == 0)
                                    throw new Exception("Gagal: Sopir tidak ditemukan atau invalid.");
                            }
                        }
                        return true;
                    }
                }
                catch (MySqlException ex) when (ex.Number == 1062)
                {
                    throw new Exception("Gagal: Username tersebut sudah digunakan.");
                }
            }
        }

        public static bool ToggleStatusSopir(int idUser, bool isActive)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();

                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand lockCmd = new MySqlCommand("SELECT id_user FROM user WHERE id_user = @id FOR UPDATE", conn, transaction))
                        {
                            lockCmd.Parameters.AddWithValue("@id", idUser);
                            lockCmd.ExecuteScalar();
                        }

                        if (!isActive)
                        {
                            string checkQuery = "SELECT COUNT(*) FROM pengiriman WHERE id_user = @id AND status_pengiriman = 'BelumTerkirim'";
                            using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@id", idUser);
                                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                                if (count > 0)
                                {
                                    throw new Exception("Tidak dapat menonaktifkan sopir. Sopir ini masih memiliki jadwal pengiriman aktif (BelumTerkirim).");
                                }
                            }
                        }

                        string query = "UPDATE user SET is_active = @status WHERE id_user = @id AND id_role = 2";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@status", isActive ? 1 : 0);
                            cmd.Parameters.AddWithValue("@id", idUser);
                            int affectedRows = cmd.ExecuteNonQuery();
                            if (affectedRows == 0)
                            {
                                // Check if user really exists
                                using (MySqlCommand checkExist = new MySqlCommand("SELECT COUNT(*) FROM user WHERE id_user = @id AND id_role = 2", conn, transaction))
                                {
                                    checkExist.Parameters.AddWithValue("@id", idUser);
                                    if (Convert.ToInt32(checkExist.ExecuteScalar()) == 0)
                                        throw new Exception("Sopir tidak ditemukan atau invalid.");
                                }
                            }
                        }
                        
                    
                        
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
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
                string query = "SELECT id_pelanggan, nama_pelanggan, alamat, no_telepon, id_wilayah, galon_dipinjam, harga_default, is_active FROM pelanggan";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static bool TambahPelanggan(string nama, string alamat, string noTelp, int idWilayah, int galonDipinjam)
        {
            if (galonDipinjam < 0)
                throw new ArgumentException("Gagal: Jumlah galon dipinjam tidak boleh negatif.");

            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO pelanggan (nama_pelanggan, alamat, no_telepon, id_wilayah, galon_dipinjam, is_active) VALUES (@nama, @alamat, @noTelp, @idWilayah, @galon, 1)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@nama", nama);
                            cmd.Parameters.AddWithValue("@alamat", string.IsNullOrEmpty(alamat) ? DBNull.Value : (object)alamat);
                            cmd.Parameters.AddWithValue("@noTelp", string.IsNullOrEmpty(noTelp) ? DBNull.Value : (object)noTelp);
                            cmd.Parameters.AddWithValue("@idWilayah", idWilayah);
                            cmd.Parameters.AddWithValue("@galon", galonDipinjam);
                            cmd.ExecuteNonQuery();
                        }

                        if (galonDipinjam > 0)
                        {
                            using (MySqlCommand checkStok = new MySqlCommand("SELECT stok_kosong_gudang FROM stok_galon WHERE id_stok = 1 FOR UPDATE", conn, transaction))
                            {
                                int stokGudang = Convert.ToInt32(checkStok.ExecuteScalar());
                                if (stokGudang < galonDipinjam)
                                    throw new Exception("Gagal: Stok kosong gudang tidak mencukupi untuk peminjaman awal pelanggan.");
                            }

                            string updateStok = "UPDATE stok_galon SET stok_kosong_gudang = stok_kosong_gudang - @galon, stok_kosong_dipinjam = stok_kosong_dipinjam + @galon WHERE id_stok = 1";
                            using (MySqlCommand cmdStok = new MySqlCommand(updateStok, conn, transaction))
                            {
                                cmdStok.Parameters.AddWithValue("@galon", galonDipinjam);
                                cmdStok.ExecuteNonQuery();
                            }
                        }
                        
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static bool EditPelanggan(int idPelanggan, string nama, string alamat, string noTelp, int idWilayah, int galonDipinjam)
        {
            if (galonDipinjam < 0)
                throw new ArgumentException("Gagal: Jumlah galon dipinjam tidak boleh negatif.");

            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int oldGalonDipinjam = 0;
                        using (MySqlCommand cmdOld = new MySqlCommand("SELECT galon_dipinjam FROM pelanggan WHERE id_pelanggan = @id FOR UPDATE", conn, transaction))
                        {
                            cmdOld.Parameters.AddWithValue("@id", idPelanggan);
                            var res = cmdOld.ExecuteScalar();
                            if (res == null) throw new Exception("Pelanggan tidak ditemukan.");
                            oldGalonDipinjam = Convert.ToInt32(res);
                        }

                        string query = "UPDATE pelanggan SET nama_pelanggan=@nama, alamat=@alamat, no_telepon=@noTelp, id_wilayah=@idWilayah, galon_dipinjam=@galon WHERE id_pelanggan=@id";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@nama", nama);
                            cmd.Parameters.AddWithValue("@alamat", string.IsNullOrEmpty(alamat) ? DBNull.Value : (object)alamat);
                            cmd.Parameters.AddWithValue("@noTelp", string.IsNullOrEmpty(noTelp) ? DBNull.Value : (object)noTelp);
                            cmd.Parameters.AddWithValue("@idWilayah", idWilayah);
                            cmd.Parameters.AddWithValue("@galon", galonDipinjam);
                            cmd.Parameters.AddWithValue("@id", idPelanggan);
                            cmd.ExecuteNonQuery();
                        }

                        int diff = galonDipinjam - oldGalonDipinjam;
                        if (diff != 0)
                        {
                            if (diff > 0)
                            {
                                using (MySqlCommand checkStok = new MySqlCommand("SELECT stok_kosong_gudang FROM stok_galon WHERE id_stok = 1 FOR UPDATE", conn, transaction))
                                {
                                    int stokGudang = Convert.ToInt32(checkStok.ExecuteScalar());
                                    if (stokGudang < diff)
                                        throw new Exception("Gagal: Stok kosong gudang tidak mencukupi untuk penambahan pinjaman ini.");
                                }
                            }
                            else if (diff < 0)
                            {
                                using (MySqlCommand checkStok = new MySqlCommand("SELECT stok_kosong_dipinjam FROM stok_galon WHERE id_stok = 1 FOR UPDATE", conn, transaction))
                                {
                                    int stokDipinjam = Convert.ToInt32(checkStok.ExecuteScalar());
                                    if (stokDipinjam < Math.Abs(diff))
                                        throw new Exception("Gagal: Sinkronisasi gagal karena jumlah galon dipinjam melebihi sirkulasi stok sistem.");
                                }
                            }

                            string updateStok = "UPDATE stok_galon SET stok_kosong_gudang = stok_kosong_gudang - @diff, stok_kosong_dipinjam = stok_kosong_dipinjam + @diff WHERE id_stok = 1";
                            using (MySqlCommand cmdStok = new MySqlCommand(updateStok, conn, transaction))
                            {
                                cmdStok.Parameters.AddWithValue("@diff", diff);
                                cmdStok.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static bool ToggleStatusPelanggan(int idPelanggan, bool isActive)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();

                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand lockCmd = new MySqlCommand("SELECT id_pelanggan FROM pelanggan WHERE id_pelanggan = @id FOR UPDATE", conn, transaction))
                        {
                            lockCmd.Parameters.AddWithValue("@id", idPelanggan);
                            lockCmd.ExecuteScalar();
                        }

                        if (!isActive)
                        {
                            string checkQuery = "SELECT COUNT(*) FROM pengiriman WHERE id_pelanggan = @id AND status_pengiriman = 'BelumTerkirim'";
                            using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@id", idPelanggan);
                                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                                if (count > 0)
                                {
                                    throw new Exception("Tidak dapat menonaktifkan pelanggan. Pelanggan ini masih memiliki jadwal pengiriman aktif (BelumTerkirim).");
                                }
                            }
                        }

                        string query = "UPDATE pelanggan SET is_active = @status WHERE id_pelanggan = @id";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@status", isActive ? 1 : 0);
                            cmd.Parameters.AddWithValue("@id", idPelanggan);
                            int affectedRows = cmd.ExecuteNonQuery();
                            if (affectedRows == 0)
                            {
                                using (MySqlCommand checkExist = new MySqlCommand("SELECT COUNT(*) FROM pelanggan WHERE id_pelanggan = @id", conn, transaction))
                                {
                                    checkExist.Parameters.AddWithValue("@id", idPelanggan);
                                    if (Convert.ToInt32(checkExist.ExecuteScalar()) == 0)
                                        throw new Exception("Pelanggan tidak ditemukan.");
                                }
                            }
                        }

                      
                        
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // ================= 4. FITUR KELOLA ARMADA =================
        public static DataTable GetAllArmada()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_wilayah, nama_wilayah, harga_pengiriman FROM wilayah";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static bool EditArmada(int idArmada, string namaWilayah, decimal hargaPengiriman)
        {
            if (hargaPengiriman < 0)
                throw new ArgumentException("Gagal: Harga pengiriman tidak boleh negatif.");

            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = "UPDATE wilayah SET nama_wilayah = @nama, harga_pengiriman = @harga WHERE id_wilayah = @id";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@nama", namaWilayah);
                            cmd.Parameters.AddWithValue("@harga", hargaPengiriman);
                            cmd.Parameters.AddWithValue("@id", idArmada);
                            
                            int affected = cmd.ExecuteNonQuery();
                            if (affected == 0)
                            {
                                using (MySqlCommand checkExist = new MySqlCommand("SELECT COUNT(*) FROM wilayah WHERE id_wilayah = @id", conn, transaction))
                                {
                                    checkExist.Parameters.AddWithValue("@id", idArmada);
                                    if (Convert.ToInt32(checkExist.ExecuteScalar()) == 0)
                                        throw new Exception("Gagal: Wilayah atau Armada tidak ditemukan.");
                                }
                            }
                        }

                        string updatePelanggan = "UPDATE pelanggan SET harga_default = @harga WHERE id_wilayah = @id";
                        using (MySqlCommand cmdPelanggan = new MySqlCommand(updatePelanggan, conn, transaction))
                        {
                            cmdPelanggan.Parameters.AddWithValue("@harga", hargaPengiriman);
                            cmdPelanggan.Parameters.AddWithValue("@id", idArmada);
                            cmdPelanggan.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // ================= 5. FITUR VIEW LAPORAN =================
        public static DataTable GetAllLaporan()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        p.id_pengiriman AS `ID Laporan`, 
                        p.tanggal_pengiriman AS `Tanggal Pengiriman`, 
                        u.nama AS `Nama Sopir`, 
                        pel.nama_pelanggan AS `Nama Pelanggan`, 
                        p.jumlah_pesanan AS `Jumlah Pesanan`, 
                        p.galon_kembali AS `Galon Kembali`, 
                        p.status_pengiriman AS `Status Pengiriman`, 
                        p.status_pembayaran AS `Status Pembayaran`, 
                        p.waktu_submit AS `Waktu Submit`
                    FROM pengiriman p
                    JOIN user u ON p.id_user = u.id_user
                    JOIN pelanggan pel ON p.id_pelanggan = pel.id_pelanggan
                    WHERE p.status_pengiriman != 'BelumTerkirim'
                    ORDER BY p.tanggal_pengiriman DESC";
                
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
        // ================= 6. FITUR ATUR JADWAL PENGIRIMAN =================
        public static DataTable GetAllJadwal()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        p.id_pengiriman AS `ID Jadwal`, 
                        p.tanggal_pengiriman AS `Tanggal`, 
                        pel.nama_pelanggan AS `Nama Pelanggan`, 
                        u.nama AS `Nama Sopir`, 
                        p.jumlah_pesanan AS `Jumlah Pesanan`,
                        p.id_pelanggan,
                        p.id_user
                    FROM pengiriman p
                    JOIN user u ON p.id_user = u.id_user
                    JOIN pelanggan pel ON p.id_pelanggan = pel.id_pelanggan
                    WHERE p.status_pengiriman = 'BelumTerkirim'
                    AND pel.is_active = 1
                    ORDER BY p.tanggal_pengiriman DESC";
                
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static bool TambahJadwal(DateTime tanggal, int idPelanggan, int idUser, int jumlahPesanan)
        {
            if (jumlahPesanan <= 0)
                throw new ArgumentException("Jumlah pesanan harus lebih dari 0.");

            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int sopirAktif;
                        using (MySqlCommand cmdSopir = new MySqlCommand("SELECT is_active FROM user WHERE id_user = @idUser AND id_role = 2 FOR UPDATE", conn, transaction))
                        {
                            cmdSopir.Parameters.AddWithValue("@idUser", idUser);
                            var res = cmdSopir.ExecuteScalar();
                            if (res == null) throw new Exception("Gagal: Sopir tidak valid atau tidak ditemukan.");
                            sopirAktif = Convert.ToInt32(res);
                        }

                        int pelangganAktif;
                        using (MySqlCommand cmdPelanggan = new MySqlCommand("SELECT is_active FROM pelanggan WHERE id_pelanggan = @idPelanggan FOR UPDATE", conn, transaction))
                        {
                            cmdPelanggan.Parameters.AddWithValue("@idPelanggan", idPelanggan);
                            var res = cmdPelanggan.ExecuteScalar();
                            if (res == null) throw new Exception("Gagal: Pelanggan tidak valid atau tidak ditemukan.");
                            pelangganAktif = Convert.ToInt32(res);
                        }
                        if (sopirAktif == 0) throw new Exception("Gagal: Sopir tersebut sudah dinonaktifkan.");
                        if (pelangganAktif == 0) throw new Exception("Gagal: Pelanggan tersebut sudah dinonaktifkan.");

                        string query = @"
                            INSERT INTO pengiriman (tanggal_pengiriman, jumlah_pesanan, is_prioritas, id_pelanggan, id_user, status_pengiriman, status_pembayaran, galon_kembali, waktu_submit) 
                            VALUES (@tgl, @jumlah, 0, @idPelanggan, @idUser, 'BelumTerkirim', 'Bon', 0, @waktuSubmit)";
                        
                        using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@tgl", tanggal.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@jumlah", jumlahPesanan);
                            cmd.Parameters.AddWithValue("@idPelanggan", idPelanggan);
                            cmd.Parameters.AddWithValue("@idUser", idUser);
                            cmd.Parameters.AddWithValue("@waktuSubmit", DBNull.Value);
                            bool result = cmd.ExecuteNonQuery() > 0;
                            transaction.Commit();
                            return result;
                        }
                    }
                    catch (MySqlException ex) when (ex.Number == 1062) // Duplicate entry
                    {
                        transaction.Rollback();
                        throw new Exception("Jadwal untuk pelanggan ini pada tanggal tersebut sudah ada.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static bool EditJadwal(int idPengiriman, DateTime tanggal, int idPelanggan, int idUser, int jumlahPesanan)
        {
            if (jumlahPesanan <= 0)
                throw new ArgumentException("Jumlah pesanan harus lebih dari 0.");

            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int sopirAktif;
                        using (MySqlCommand cmdSopir = new MySqlCommand("SELECT is_active FROM user WHERE id_user = @idUser AND id_role = 2 FOR UPDATE", conn, transaction))
                        {
                            cmdSopir.Parameters.AddWithValue("@idUser", idUser);
                            var res = cmdSopir.ExecuteScalar();
                            if (res == null) throw new Exception("Gagal: Sopir tidak valid atau tidak ditemukan.");
                            sopirAktif = Convert.ToInt32(res);
                        }

                        int pelangganAktif;
                        using (MySqlCommand cmdPelanggan = new MySqlCommand("SELECT is_active FROM pelanggan WHERE id_pelanggan = @idPelanggan FOR UPDATE", conn, transaction))
                        {
                            cmdPelanggan.Parameters.AddWithValue("@idPelanggan", idPelanggan);
                            var res = cmdPelanggan.ExecuteScalar();
                            if (res == null) throw new Exception("Gagal: Pelanggan tidak valid atau tidak ditemukan.");
                            pelangganAktif = Convert.ToInt32(res);
                        }
                        if (sopirAktif == 0) throw new Exception("Gagal: Sopir tersebut sudah dinonaktifkan.");
                        if (pelangganAktif == 0) throw new Exception("Gagal: Pelanggan tersebut sudah dinonaktifkan.");

                        string query = @"
                            UPDATE pengiriman 
                            SET tanggal_pengiriman = @tgl, id_pelanggan = @idPelanggan, id_user = @idUser, jumlah_pesanan = @jumlah
                            WHERE id_pengiriman = @id AND status_pengiriman = 'BelumTerkirim'";
                        
                        using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@tgl", tanggal.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@idPelanggan", idPelanggan);
                            cmd.Parameters.AddWithValue("@idUser", idUser);
                            cmd.Parameters.AddWithValue("@jumlah", jumlahPesanan);
                            cmd.Parameters.AddWithValue("@id", idPengiriman);
                            bool result = cmd.ExecuteNonQuery() > 0;
                            transaction.Commit();
                            return result;
                        }
                    }
                    catch (MySqlException ex) when (ex.Number == 1062) // Duplicate entry
                    {
                        transaction.Rollback();
                        throw new Exception("Jadwal untuk pelanggan ini pada tanggal tersebut sudah ada.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static bool HapusJadwal(int idPengiriman)
        {
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM pengiriman WHERE id_pengiriman = @id AND status_pengiriman = 'BelumTerkirim'";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPengiriman);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static DataTable GetListSopirAktif()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_user, nama FROM user WHERE id_role = 2 AND is_active = 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static DataTable GetListPelangganAktif()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = Koneksi.Instance.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_pelanggan, nama_pelanggan FROM pelanggan WHERE is_active = 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // ================= 7. FITUR LOG AKTIVITAS =================
        public static bool LogAktivitas(string deskripsi)
        {
            if (string.IsNullOrWhiteSpace(deskripsi)) return false;

            try
            {
                using (MySqlConnection conn = Koneksi.Instance.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO log_aktivitas (deskripsi_aksi) VALUES (@deskripsi)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@deskripsi", deskripsi);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gagal menyimpan log: " + ex.Message);
                return false;
            }
        }

        public static DataTable GetLogAktivitas()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = Koneksi.Instance.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id_log AS `ID Log`, deskripsi_aksi AS `Aktivitas`, waktu AS `Waktu` FROM log_aktivitas ORDER BY waktu DESC";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
              
            }
            return dt;
        }
    }
}