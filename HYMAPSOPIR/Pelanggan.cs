using System;
using System.Collections.Generic;
using System.Text;

namespace HYMAPSOPIR
{
    public class Pelanggan
    {
      
        public string IdPelanggan {
            get; 
        }
        public string NamaPelanggan { 
            get; 
        }
        public string Alamat { 
            get; 
        }

        public Armada Wilayah { 
            get; 
        }

        public DateTime TanggalTerakhirKirim { 
            get; 
            private set; 
        }

        public Pelanggan(string id, string nama, string alamat, Armada wilayah, DateTime terakhirKirim)
        {
            IdPelanggan = id;
            NamaPelanggan = nama;
            Alamat = alamat;
            TanggalTerakhirKirim = terakhirKirim;
            Wilayah = wilayah;
        }

        // Method internal yang hanya dipanggil oleh sistem saat pengiriman selesai
        public void UpdateTanggalPengirimanBerhasil(DateTime tanggalKirimBaru)
        {
            TanggalTerakhirKirim = tanggalKirimBaru;
        }

        // Menghitung jadwal berikutnya (Siklus 7 Hari)
        public DateTime JadwalBerikutnya()
        {
            return TanggalTerakhirKirim.AddDays(7);
        }
    }
}

