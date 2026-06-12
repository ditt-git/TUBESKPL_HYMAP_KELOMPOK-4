using System;

namespace HYMAPSOPIR
{
    public class NotificationService
    {
        private static NotificationService _instance;
        public static NotificationService Instance => _instance ??= new NotificationService();

        public event EventHandler<string> PesanBaruMasuk;

        public void KirimNotifikasi(string pesan)
        {
            PesanBaruMasuk?.Invoke(this, pesan);
        }
    }
}