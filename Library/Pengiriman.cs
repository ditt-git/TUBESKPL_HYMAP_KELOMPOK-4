using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace HYMAPSOPIR
{
    public class Pengiriman
    {
        public Pelanggan DataPelanggan { get; }
        public StatusPengiriman StatusKirim { get; set; }
        public StatusPembayaran StatusBayar { get; set; }
        public PrioritasPengiriman Prioritas { get; private set; }
        public string BuktiFoto { get; set; }

        public Pengiriman(Pelanggan pelanggan, DateTime tanggalHariIni)
        {
            // Design by Contract: Pre-conditions
            Debug.Assert(pelanggan != null, "Pelanggan tidak boleh null!");
            if (pelanggan == null) throw new ArgumentNullException(nameof(pelanggan), "Data pelanggan tidak valid.");

            DataPelanggan = pelanggan;
            StatusKirim = (StatusPengiriman)0;
            StatusBayar = StatusPembayaran.Bon;
            BuktiFoto = string.Empty;

            // Kalkulasi prioritas saat pesanan di-generate hari ini
            UpdatePrioritas(tanggalHariIni);
        }

        public void UpdatePrioritas(DateTime tanggalHariIni)
        {
            Prioritas = PrioritasChecker.HitungPrioritas(DataPelanggan.JadwalBerikutnya(), tanggalHariIni);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace HYMAPSOPIR
{
    public class Pengiriman
    {
        public Pelanggan DataPelanggan { get; }
        public StatusPengiriman StatusKirim { get; set; }
        public StatusPembayaran StatusBayar { get; set; }
        public PrioritasPengiriman Prioritas { get; private set; }
        public string BuktiFoto { get; set; }

        public Pengiriman(Pelanggan pelanggan, DateTime tanggalHariIni)
        {
            // Design by Contract: Pre-conditions
            Debug.Assert(pelanggan != null, "Pelanggan tidak boleh null!");
            if (pelanggan == null) throw new ArgumentNullException(nameof(pelanggan), "Data pelanggan tidak valid.");

            DataPelanggan = pelanggan;
            StatusKirim = (StatusPengiriman)0;
            StatusBayar = StatusPembayaran.Bon;
            BuktiFoto = string.Empty;

            // Kalkulasi prioritas saat pesanan di-generate hari ini
            UpdatePrioritas(tanggalHariIni);
        }

        public void UpdatePrioritas(DateTime tanggalHariIni)
        {
            Prioritas = PrioritasChecker.HitungPrioritas(DataPelanggan.JadwalBerikutnya(), tanggalHariIni);
        }
    }
}
