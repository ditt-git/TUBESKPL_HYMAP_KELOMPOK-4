namespace HYMAPSOPIR
{
    public class NotifSopir
    {
        public static string PengumumanTerbaru = "TEST BUAT OBSERVER: Besok libur yeay 67!";

        // OBSERVER
        public static void PengumumanSopir()
        {
            if (!string.IsNullOrEmpty(PengumumanTerbaru))
            {
                // observer kirim ke subscriber
                NotificationService.Instance.KirimNotifikasi(PengumumanTerbaru);
            }
        }
    }
}
