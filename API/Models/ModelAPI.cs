using HYMAPSOPIR;

namespace API.Models
{
        public class UpdatePengiriman
        {
            public StatusPengiriman StatusKirim { get; set; }
            public StatusPembayaran StatusBayar { get; set; }

        }
        public class TambahTugas
        {
            public string IdPelanggan { get; set; }
            public DateTime TanggalTugas { get; set; }
        }
}
