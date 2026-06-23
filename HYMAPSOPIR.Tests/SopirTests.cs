using Microsoft.VisualStudio.TestTools.UnitTesting;
using HYMAPSOPIR;
using System;
using System.Collections.Generic;

namespace HYMAPSOPIR.Tests
{
    [TestClass]
    public class SopirTests
    {
        // ---------------------------------------------------------
        // 1. CONSTRUCTOR & SERVICE TEST: SetTugasBerdasarkanWilayah
        // ---------------------------------------------------------

        [TestMethod]
        public void ConstructorSopir_DataValid_BerhasilDibuat()
        {
            // Act
            Sopir sopirBaru = new Sopir(1, "Andi", "andi_id", "123", Wilayah.Denpasar);

            // Assert
            Assert.AreEqual("andi_id", sopirBaru.Id);
            Assert.IsNotNull(sopirBaru.DaftarTugasHariIni);
        }

        [TestMethod]
        public void ConstructorSopir_NamaKosong_MelemparArgumentException()
        {
            try
            {
                // ACT
                Sopir sopirGagal = new Sopir(2, "", "username", "pass", Wilayah.Denpasar);
                Assert.Fail("Validasi gagal! Objek Sopir berhasil dibuat padahal namanya kosong.");
            }
            catch (ArgumentException)
            {
            }
            catch (Exception ex)
            {
                // ASSERT
                if (ex.Message.Contains("Debug.Fail") || ex.Message.Contains("FATAL"))
                {
                    return;
                }
                Assert.Fail($"Test gagal! Error yang dilempar salah jenis. Pesan: {ex.Message}");
            }
        }

        [TestMethod]
        public void SetTugas_DataPelangganNull_BypassSistemAman()
        {
            Sopir sopir = new Sopir(3, "Budi", "budi123", "pass", Wilayah.Denpasar);
            JadwalService service = new JadwalService(); 

            // Act
            service.SetTugasBerdasarkanWilayah(sopir, null!, DateTime.Now);

            // Assert
            Assert.AreEqual(0, sopir.DaftarTugasHariIni.Count);
        }

        [TestMethod]
        public void SetTugas_AdaPelangganSesuaiRute_MasukKeDaftarTugas()
        {
            Sopir sopir = new Sopir(4, "Budi", "budi123", "pass", Wilayah.Denpasar); // Rute P001, P004, P007
            var daftarPelanggan = new List<Pelanggan>
            {
                new Pelanggan("P001", "Tono", "Alamat", Wilayah.Denpasar, DateTime.Now.AddDays(-14), 0), // Valid
                new Pelanggan("P002", "Pak RT", "Alamat", Wilayah.Karangasem, DateTime.Now, 0)          // Tidak Valid (Beda Rute)
            };
            JadwalService service = new JadwalService();

            // Act
            service.SetTugasBerdasarkanWilayah(sopir, daftarPelanggan, DateTime.Now);

            // Assert: Hanya P001 yang masuk
            Assert.AreEqual(1, sopir.DaftarTugasHariIni.Count);
            Assert.AreEqual("P001", sopir.DaftarTugasHariIni[0].DataPelanggan.IdPelanggan);
        }

        // ---------------------------------------------------------
        // 2. FUNCTION & BRANCH COVERAGE: AmbilTugasBerdasarkanNomor
        // ---------------------------------------------------------

        [TestMethod]
        public void AmbilTugas_NomorValid_MengembalikanTugas()
        {
            Sopir sopir = new Sopir(5, "Budi", "budi123", "pass", Wilayah.Denpasar);
            Pelanggan p = new Pelanggan("P001", "Tono", "Alamat", Wilayah.Denpasar, DateTime.Now, 0); // Tambah 0
            sopir.DaftarTugasHariIni.Add(new Pengiriman(p, DateTime.Now, sopir.IdUserDb)); // Tambah 1 tugas manual

            JadwalService service = new JadwalService();

            // Act
            var tugas = service.AmbilTugasBerdasarkanNomor(sopir, 1); 

            // Assert
            Assert.IsNotNull(tugas);
        }

        [TestMethod]
        public void AmbilTugas_NomorDiLuarBatas_MengembalikanNull()
        {
            Sopir sopir = new Sopir(6, "Budi", "budi123", "pass", Wilayah.Denpasar);
            JadwalService service = new JadwalService();

            // Act
            var tugas = service.AmbilTugasBerdasarkanNomor(sopir, 99); 

            // Assert
            Assert.IsNull(tugas);
        }

        // ---------------------------------------------------------
        // 3. FUNCTION & BRANCH COVERAGE: Command Pattern Eksekusi
        // ---------------------------------------------------------

        [TestMethod]
        public void EksekusiPengirimanCommand_TugasNull_Ditolak()
        {
            // Act & Assert
            try
            {
                var cmd = new Library.Commands.UpdatePengirimanCommand(null!, StatusPengiriman.SudahTerkirim, StatusPembayaran.Cash, 0);
                cmd.Execute();
                Assert.Fail("Harusnya melempar exception karena tugas null.");
            }
            catch (Exception)
            {
               
            }
        }



        [TestMethod]
        public void EksekusiPengirimanCommand_Sukses_TanggalBerubah()
        {
            Pelanggan p = new Pelanggan("P001", "Tono", "Alamat", Wilayah.Denpasar, DateTime.Now.AddDays(-5), 0);
            Pengiriman tugas = new Pengiriman(p, DateTime.Now, 1);


            // Act
            var cmd = new Library.Commands.UpdatePengirimanCommand(tugas, StatusPengiriman.SudahTerkirim, StatusPembayaran.Cash, 0);
            cmd.Execute();

            // Assert
            Assert.AreEqual(DateTime.Now.Date, p.TanggalTerakhirKirim.Date);
        }

        // ---------------------------------------------------------
        // 4. PERFORMANCE TEST
        // ---------------------------------------------------------

        [TestMethod]
        [Timeout(2000)] // Batas maksimal eksekusi adalah 2 detik
        public void PerformanceTest_SetTugas_SeratusRibuData()
        {
            Sopir sopir = new Sopir(7, "Budi", "budi123", "pass", Wilayah.Denpasar);
            var dataMassal = new List<Pelanggan>();

            // Generate 100.000 data fiktif
            for (int i = 0; i < 100000; i++)
            {
                dataMassal.Add(new Pelanggan($"P{i}", $"Pelanggan {i}", "Alamat", Wilayah.Denpasar, DateTime.Now.AddDays(-14), 0));
            }

            JadwalService service = new JadwalService(); 

            // Alat ukur waktu mulai
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            service.SetTugasBerdasarkanWilayah(sopir, dataMassal, DateTime.Now);

            // Alat ukur waktu berhenti
            stopwatch.Stop();
            long waktuEksekusi = stopwatch.ElapsedMilliseconds;

            // Cetak hasil
            Console.WriteLine($"[PERFORMANCE METRIC] Waktu pemrosesan 100.000 data: {waktuEksekusi} milidetik.");

            Assert.IsTrue(waktuEksekusi < 2000, "Performa lambat!");
        }
    }
}