using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace HYMAPSOPIR
{
  
    public class Pelanggan : EntitasDasar<string>
    {


        public const int SIKLUS_HARI_PENGIRIMAN = 7;

        public string IdPelanggan => Id;

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
            // Design by Contract: Pre-conditions
            Debug.Assert(!string.IsNullOrWhiteSpace(id), "Id tidak boleh kosong!");
            Debug.Assert(!string.IsNullOrWhiteSpace(nama), "Nama tidak boleh kosong!");
            Debug.Assert(!string.IsNullOrWhiteSpace(alamat), "Alamat tidak boleh kosong!");

            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id pelanggan tidak valid.");
            if (string.IsNullOrWhiteSpace(nama)) throw new ArgumentException("Nama pelanggan tidak valid.");
            if (string.IsNullOrWhiteSpace(alamat)) throw new ArgumentException("Alamat pelanggan tidak valid.");

            Id = id; 
            NamaPelanggan = nama;
            Alamat = alamat;
            TanggalTerakhirKirim = terakhirKirim;
            Wilayah = wilayah;
        }

    
        public void UpdateTanggalPengirimanBerhasil(DateTime tanggalKirimBaru)
        {
            // Design by Contract: Pre-condition
            Debug.Assert(tanggalKirimBaru >= TanggalTerakhirKirim, "Tanggal pengiriman baru tidak boleh sebelum tanggal terakhir kirim!");
            if (tanggalKirimBaru < TanggalTerakhirKirim)
            {
                throw new ArgumentException("Tanggal pengiriman tidak valid. Harus lebih baru atau sama dengan tanggal pengiriman sebelumnya.");
            }

            TanggalTerakhirKirim = tanggalKirimBaru;
        }

        // Menghitung jadwal berikutnya (Siklus 7 Hari)
        public DateTime JadwalBerikutnya()
        {
            return TanggalTerakhirKirim.AddDays(SIKLUS_HARI_PENGIRIMAN);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace HYMAPSOPIR
{
  
    public class Pelanggan : EntitasDasar<string>
    {


        public const int SIKLUS_HARI_PENGIRIMAN = 7;

        public string IdPelanggan => Id;

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
        public int GalonDipinjam { get; private set; }
        public Pelanggan(string id, string nama, string alamat, Armada wilayah, DateTime terakhirKirim, int galonDipinjam)
        {
            // Design by Contract: Pre-conditions
            Debug.Assert(!string.IsNullOrWhiteSpace(id), "Id tidak boleh kosong!");
            Debug.Assert(!string.IsNullOrWhiteSpace(nama), "Nama tidak boleh kosong!");
            Debug.Assert(!string.IsNullOrWhiteSpace(alamat), "Alamat tidak boleh kosong!");

            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id pelanggan tidak valid.");
            if (string.IsNullOrWhiteSpace(nama)) throw new ArgumentException("Nama pelanggan tidak valid.");
            if (string.IsNullOrWhiteSpace(alamat)) throw new ArgumentException("Alamat pelanggan tidak valid.");

            Id = id; 
            NamaPelanggan = nama;
            Alamat = alamat;
            TanggalTerakhirKirim = terakhirKirim;
            Wilayah = wilayah;
            GalonDipinjam = galonDipinjam;
        }

    
        public void UpdateTanggalPengirimanBerhasil(DateTime tanggalKirimBaru)
        {
            // Design by Contract: Pre-condition
            Debug.Assert(tanggalKirimBaru >= TanggalTerakhirKirim, "Tanggal pengiriman baru tidak boleh sebelum tanggal terakhir kirim!");
            if (tanggalKirimBaru < TanggalTerakhirKirim)
            {
                throw new ArgumentException("Tanggal pengiriman tidak valid. Harus lebih baru atau sama dengan tanggal pengiriman sebelumnya.");
            }

            TanggalTerakhirKirim = tanggalKirimBaru;
        }

        // Menghitung jadwal berikutnya (Siklus 7 Hari)
        public DateTime JadwalBerikutnya()
        {
            return TanggalTerakhirKirim.AddDays(SIKLUS_HARI_PENGIRIMAN);
        }
    }
}

