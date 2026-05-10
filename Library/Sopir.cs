using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace HYMAPSOPIR
{

    public class EntitasDasar<TId>
    {
        public TId Id { get; protected set; }
    }

    public class Sopir : EntitasDasar<string>
    {


        public string Nama { get; }
        public string Username { get; }
        public string Password { get; }
        public Armada ArmadaTugas { get; }
        public List<Pengiriman> DaftarTugasHariIni;

        public Sopir(string nama, string username, string password, Armada armada)
        {
            Id = username;
            Nama = nama;
            Username = username;
            Password = password;
            ArmadaTugas = armada;
            DaftarTugasHariIni = new List<Pengiriman>();

            Debug.Assert(!string.IsNullOrWhiteSpace(nama), "Nama tidak boleh kosong!");
            if (string.IsNullOrWhiteSpace(nama)) throw new ArgumentException("Nama tidak sesuai!.");
        }

        public void SetTugasBerdasarkanArmada(List<Pelanggan> semuaPelanggan, DateTime hariIni)
        {
            string[] ruteId = RouteTable.GetRute(this.ArmadaTugas);

            var pelangganTarget = semuaPelanggan
                .Where(p => ruteId.Contains(p.IdPelanggan) &&
                            (p.JadwalBerikutnya() <= hariIni || p.TanggalTerakhirKirim.Date == hariIni.Date))
                .ToList();

            var tugasBaruAtauLama = new List<Pengiriman>();

            foreach (var p in pelangganTarget)
            {
                var tugasSudahAda = DaftarTugasHariIni.Find(t => t.DataPelanggan.IdPelanggan == p.IdPelanggan);

                if (tugasSudahAda != null)
                {
                    tugasSudahAda.UpdatePrioritas(hariIni);
                    tugasBaruAtauLama.Add(tugasSudahAda); 
                }
                else
                {
                    tugasBaruAtauLama.Add(new Pengiriman(p, hariIni));
                }
            }

            DaftarTugasHariIni = tugasBaruAtauLama.OrderByDescending(t => t.Prioritas).ToList();
        }

        

        public void TampilkanJadwalHariIni()
        {
            Console.WriteLine($"\n=== JADWAL PENGIRIMAN HARI INI: {Nama} ===");
            Console.WriteLine($"ARMADA: {ArmadaTugas}");
            if (DaftarTugasHariIni.Count == 0)
            {
                Console.WriteLine("Tidak ada jadwal pengiriman untuk wilayah Anda hari ini.");
            }
            else
            {
                for (int i = 0; i < DaftarTugasHariIni.Count; i++)
                {
                    var tugas = DaftarTugasHariIni[i];
                    Console.WriteLine($"[{i + 1}] {tugas.DataPelanggan.NamaPelanggan} | {tugas.DataPelanggan.Alamat}");
                    Console.WriteLine($"    Prioritas : {tugas.Prioritas}");
                    Console.WriteLine($"    Kirim     : {tugas.StatusKirim}");
                    Console.WriteLine($"    Bayar     : {tugas.StatusBayar}");
                    Console.WriteLine("--------------------------------------------");
                }
            }
        }

        public Pengiriman AmbilTugasBerdasarkanNomor(int nomor)
        {
            if (nomor > 0 && nomor <= DaftarTugasHariIni.Count)
            {
                return DaftarTugasHariIni[nomor - 1];
            }
            return null;
        }

        public void EksekusiPengiriman(Pengiriman tugas, StatusPengiriman statusKirim, StatusPembayaran statusBayar, string foto = "")
        {
            if (statusKirim == StatusPengiriman.SudahTerkirim && string.IsNullOrEmpty(foto))
            {
                Console.WriteLine($"\n[GAGAL] Validasi ditolak! Bukti foto wajib diisi jika galon terkirim.");
                return;
            }

            tugas.StatusKirim = statusKirim;
            tugas.StatusBayar = statusBayar;
            tugas.BuktiFoto = foto;

            Console.WriteLine($"\n[SUKSES] Data {tugas.DataPelanggan.NamaPelanggan} berhasil di-update!");

            if (statusKirim == StatusPengiriman.SudahTerkirim)
            {
                tugas.DataPelanggan.UpdateTanggalPengirimanBerhasil(DateTime.Now);
            }
        }

        public void TampilkanJadwalMendatang(List<Pelanggan> semuaPelanggan, DateTime tanggalHariIni, int rentangHari = 7)
        {
            Console.WriteLine($"\n=== PROYEKSI JADWAL ({rentangHari} HARI KE DEPAN) ===");
            string[] ruteId = RouteTable.GetRute(this.ArmadaTugas);

            var pelangganDiurutkan = semuaPelanggan
                .Where(p => ruteId.Contains(p.IdPelanggan))
                .OrderBy(p => p.JadwalBerikutnya())
                .ToList();

            bool adaJadwal = false;
            foreach (var p in pelangganDiurutkan)
            {
                DateTime jadwal = p.JadwalBerikutnya();
                if (jadwal > tanggalHariIni && jadwal <= tanggalHariIni.AddDays(rentangHari))
                {
                    Console.WriteLine($"- {jadwal.ToString("dd MMM yyyy")} : {p.NamaPelanggan} | {p.Alamat}");
                    adaJadwal = true;
                }
            }

            if (!adaJadwal) Console.WriteLine("Tidak ada jadwal pengiriman di wilayah Anda.");
            Console.WriteLine("====================================================\n");
        }
    }
}