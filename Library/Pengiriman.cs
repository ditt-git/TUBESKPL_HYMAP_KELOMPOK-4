using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace HYMAPSOPIR
{
    public class Pengiriman : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private StatusPengiriman _statusKirim;

        public int IdUserSopir { get; set; }
        public Pelanggan DataPelanggan { get; }
        public StatusPengiriman StatusKirim
        {
            get { return _statusKirim; }
            set
            {
                _statusKirim = value;
                if (_statusKirim == (StatusPengiriman)1)
                {
                    Notify();
                }
            }
        }
        public StatusPembayaran StatusBayar { get; set; }
        public PrioritasPengiriman Prioritas { get; set; }

        public int GalonKembali { get; set; }
        public DateTime TanggalTugas { get; set; }
        public Pengiriman(Pelanggan pelanggan, DateTime tanggalHariIni, int idUserSopir)
        {
            // Design by Contract: Pre-conditions
            Debug.Assert(pelanggan != null, "Pelanggan tidak boleh null!");
            if (pelanggan == null) throw new ArgumentNullException(nameof(pelanggan), "Data pelanggan tidak valid.");

            IdUserSopir = idUserSopir;
            DataPelanggan = pelanggan;
            StatusKirim = (StatusPengiriman)0;
            StatusBayar = StatusPembayaran.Bon;

            GalonKembali = 0;
            TanggalTugas = tanggalHariIni;

            // Kalkulasi prioritas saat pesanan di-generate hari ini
            UpdatePrioritas(tanggalHariIni);
        }

        public void UpdatePrioritas(DateTime tanggalHariIni)
        {
            Prioritas = PrioritasChecker.HitungPrioritas(DataPelanggan.JadwalBerikutnya(), tanggalHariIni);
        }


        // Subjek Observer Pattern
        public void Attach(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update($"Pengiriman ke {DataPelanggan.NamaPelanggan} Selesai!");
            }
        }
    }
}
