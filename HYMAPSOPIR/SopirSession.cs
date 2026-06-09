using System;

namespace HYMAPSOPIR
{
    public class SopirSession
    {
        private static SopirSession _instance;

        // Properti untuk menyimpan objek sopir yang sedang aktif/login
        public Sopir SopirAktif { get; set; }

        private SopirSession()
        {
        }

        public static SopirSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SopirSession();
                }
                return _instance;
            }
        }

        // Fungsi tambahan untuk membersihkan data saat logout
        public void Logout()
        {
            SopirAktif = null;
        }
    }
}