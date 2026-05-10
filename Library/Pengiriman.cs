using System;
using System.Collections.Generic;
using System.Text;

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
            DataPelanggan = pelanggan;
            StatusKirim = StatusPengiriman.BelumTerkirim;
            StatusBayar = StatusPembayaran.BelumBayar;
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
