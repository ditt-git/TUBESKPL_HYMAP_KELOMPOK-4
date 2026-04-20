using HYMAPSOPIR;
using System;
using System.Collections.Generic;

namespace HYMAPSOPIR
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime hariIni = new DateTime(2026, 3, 29);

            // 1. Inisialisasi Sopir dengan Armada masing-masing
            List<Sopir> databaseSopir = new List<Sopir>
            {
                // Aditya terikat khusus armada Denpasar
                new Sopir("A", "aditya", "12345", Armada.Denpasar),
                //new Sopir("Pak Joko", "joko", "galon123", Armada.Gianyar)
            };

            // 2. Inisialisasi Pelanggan dengan Wilayah
            List<Pelanggan> databasePelanggan = new List<Pelanggan>
            {
                new Pelanggan("P001", "Tono", "Jl. Teuku Umar", Armada.Denpasar, new DateTime(2026, 3, 22)),
                new Pelanggan("P002", "Pak RT", "Amlapura", Armada.Karangasem, new DateTime(2026, 3, 20)),
                new Pelanggan("P003", "Budi", "Ubud", Armada.Gianyar, new DateTime(2026, 3, 21)),
                new Pelanggan("P004", "Siti", "Renon", Armada.Denpasar, new DateTime(2026, 3, 22))
            };

            // ===== SISTEM LOGIN =====
            Sopir sopirAktif = null; // Menyimpan data sopir yang berhasil login
            bool isLoginSukses = false;

            Console.WriteLine("==================================");
            Console.WriteLine("   SELAMAT DATANG DI HYMAP APP");
            Console.WriteLine("==================================");

            while (!isLoginSukses)
            {
                Console.Write("Username : ");
                string inputUser = Console.ReadLine();
                Console.Write("Password : ");
                string inputPass = Console.ReadLine();

                // Cek apakah username dan password cocok dengan database
                foreach (var s in databaseSopir)
                {
                    if (s.Username == inputUser && s.Password == inputPass)
                    {
                        sopirAktif = s;
                        isLoginSukses = true;
                        break;
                    }
                }

                if (!isLoginSukses)
                {
                    Console.WriteLine("[ERROR] Username atau Password salah. Silakan coba lagi.\n");
                }
            }

            Console.WriteLine($"\n[SUKSES] Login berhasil. Selamat bekerja, {sopirAktif.Nama}!");
            // ========================

            sopirAktif.SetTugasBerdasarkanArmada(databasePelanggan, hariIni);

            // ===== LOOP APLIKASI INTERAKTIF (DASHBOARD) =====
            bool aplikasiBerjalan = true;
            while (aplikasiBerjalan)
            {
                Console.WriteLine("\n==================================");
                Console.WriteLine($"   DASHBOARD SOPIR : {sopirAktif.Nama.ToUpper()}");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Cek Pelanggan / Jadwal Hari Ini");
                Console.WriteLine("2. Validasi Pengiriman & Pembayaran");
                Console.WriteLine("3. Cek Jadwal Mendatang (Proyeksi Mingguan)");
                Console.WriteLine("4. Logout (Keluar)");
                Console.Write("Pilih menu (1/2/3/4): ");

                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        sopirAktif.TampilkanJadwalHariIni();
                        break;

                    case "2":
                        sopirAktif.TampilkanJadwalHariIni();
                        Console.Write("Masukkan Nomor Pelanggan yang ingin divalidasi: ");

                        if (int.TryParse(Console.ReadLine(), out int nomorPilih))
                        {
                            Pengiriman tugasDipilih = sopirAktif.AmbilTugasBerdasarkanNomor(nomorPilih);

                            if (tugasDipilih != null)
                            {
                                Console.WriteLine($"\n>> Validasi untuk: {tugasDipilih.DataPelanggan.NamaPelanggan}");

                                Console.WriteLine("Pilih Status Pengiriman:");
                                Console.WriteLine("0 = Belum Terkirim, 1 = Sudah Terkirim, 2 = Gagal");
                                Console.Write("Input (0/1/2): ");
                                int inputKirim = int.Parse(Console.ReadLine());
                                StatusPengiriman statusKirimBaru = (StatusPengiriman)inputKirim;

                                string fotoBukti = "";
                                if (statusKirimBaru == StatusPengiriman.SudahTerkirim)
                                {
                                    Console.Write("Masukkan nama file bukti foto (contoh: foto.jpg): ");
                                    fotoBukti = Console.ReadLine();
                                }

                                Console.WriteLine("\nPilih Status Pembayaran:");
                                Console.WriteLine("0 = Belum Bayar, 1 = Cash, 2 = Transfer, 3 = Bon");
                                Console.Write("Input (0/1/2/3): ");
                                int inputBayar = int.Parse(Console.ReadLine());
                                StatusPembayaran statusBayarBaru = (StatusPembayaran)inputBayar;

                                sopirAktif.EksekusiPengiriman(tugasDipilih, statusKirimBaru, statusBayarBaru, fotoBukti);
                            }
                            else
                            {
                                Console.WriteLine("Nomor pelanggan tidak ditemukan!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Input tidak valid. Harap masukkan angka.");
                        }
                        break;

                    case "3":
                        // melempar databasePelanggan, tanggal hari ini, dan proyeksi 7 hari ke depan
                        sopirAktif.TampilkanJadwalMendatang(databasePelanggan, hariIni, 7);
                        break;

                    case "4":
                        aplikasiBerjalan = false;
                        Console.WriteLine($"Logout berhasil. Terima kasih atas kerja kerasmu hari ini, {sopirAktif.Nama}!");
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak ada di menu.");
                        break;
                }
            }
        }
    }
}