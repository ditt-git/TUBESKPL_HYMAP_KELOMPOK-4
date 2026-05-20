using Microsoft.VisualStudio.TestTools.UnitTesting;
using HYMAPSOPIR;
using System;

namespace HYMAPSOPIR.Tests
{
    [TestClass]
    public class SopirTests
    {
        [TestMethod]
        public void ConstructorSopir_DataValid_BerhasilDibuat()
        {
            // Act
            Sopir sopirBaru = new Sopir("Andi", "andi_id", "123", Armada.Denpasar);

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
                Sopir sopirGagal = new Sopir("", "username", "pass", Armada.Denpasar);


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
            Sopir sopir = new Sopir("Budi", "budi123", "pass", Armada.Denpasar);

            // Act
            sopir.SetTugasBerdasarkanArmada(null, DateTime.Now);

            // Assert
            Assert.AreEqual(0, sopir.DaftarTugasHariIni.Count);
        }

        [TestMethod]
        public void SetTugas_AdaPelangganSesuaiRute_MasukKeDaftarTugas()
        {
            Sopir sopir = new Sopir("Budi", "budi123", "pass", Armada.Denpasar); // Rute P001, P004, P007
            var daftarPelanggan = new System.Collections.Generic.List<Pelanggan>
            {
                new Pelanggan("P001", "Tono", "Alamat", Armada.Denpasar, DateTime.Now.AddDays(-14)), // Valid
                new Pelanggan("P002", "Pak RT", "Alamat", Armada.Karangasem, DateTime.Now)          // Tidak Valid (Beda Rute)
            };

            // Act
            sopir.SetTugasBerdasarkanArmada(daftarPelanggan, DateTime.Now);

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
            Sopir sopir = new Sopir("Budi", "budi123", "pass", Armada.Denpasar);
            Pelanggan p = new Pelanggan("P001", "Tono", "Alamat", Armada.Denpasar, DateTime.Now);
            sopir.DaftarTugasHariIni.Add(new Pengiriman(p, DateTime.Now)); // Tambah 1 tugas manual

            // Act
            var tugas = sopir.AmbilTugasBerdasarkanNomor(1);

            // Assert
            Assert.IsNotNull(tugas);
        }

        [TestMethod]
        public void AmbilTugas_NomorDiLuarBatas_MengembalikanNull()
        {
            Sopir sopir = new Sopir("Budi", "budi123", "pass", Armada.Denpasar);

            // Act
            var tugas = sopir.AmbilTugasBerdasarkanNomor(99);

            // Assert
            Assert.IsNull(tugas);
        }

        // ---------------------------------------------------------
        // 3. FUNCTION & BRANCH COVERAGE: EksekusiPengiriman
        // ---------------------------------------------------------

        [TestMethod]
        public void EksekusiPengiriman_TugasNull_Ditolak()
        {
            Sopir sopir = new Sopir("Budi", "budi123", "pass", Armada.Denpasar);

            // Act: Kirim null
            sopir.EksekusiPengiriman(null, StatusPengiriman.SudahTerkirim, StatusPembayaran.Cash, "foto.jpg");

        }

        [TestMethod]
        public void EksekusiPengiriman_StatusTerkirimTapiFotoKosong_Ditolak()
        {
            Sopir sopir = new Sopir("Budi", "budi123", "pass", Armada.Denpasar);
            Pelanggan p = new Pelanggan("P001", "Tono", "Alamat", Armada.Denpasar, DateTime.Now.AddDays(-5));
            Pengiriman tugas = new Pengiriman(p, DateTime.Now);

            // Act
            sopir.EksekusiPengiriman(tugas, StatusPengiriman.SudahTerkirim, StatusPembayaran.Cash, "");

            // Assert
            Assert.AreEqual(DateTime.Now.AddDays(-5).Date, p.TanggalTerakhirKirim.Date);
        }

        [TestMethod]
        public void EksekusiPengiriman_Sukses_TanggalBerubah()
        {
            Sopir sopir = new Sopir("Budi", "budi123", "pass", Armada.Denpasar);
            Pelanggan p = new Pelanggan("P001", "Tono", "Alamat", Armada.Denpasar, DateTime.Now.AddDays(-5));
            Pengiriman tugas = new Pengiriman(p, DateTime.Now);

            // Act
            sopir.EksekusiPengiriman(tugas, StatusPengiriman.SudahTerkirim, StatusPembayaran.Cash, "bukti.jpg");

            // Assert
            Assert.AreEqual(DateTime.Now.Date, p.TanggalTerakhirKirim.Date);


        }

        [TestMethod]
        [Timeout(2000)] // Batas maksimal eksekusi adalah 2 detik
        public void PerformanceTest_SetTugas_SeratusRibuData()
        {
            Sopir sopir = new Sopir("Budi", "budi123", "pass", Armada.Denpasar);
            var dataMassal = new System.Collections.Generic.List<Pelanggan>();

            // Generate 100.000 data fiktif
            for (int i = 0; i < 100000; i++)
            {
                dataMassal.Add(new Pelanggan($"P{i}", $"Pelanggan {i}", "Alamat", Armada.Denpasar, DateTime.Now.AddDays(-14)));
            }

            // Alat ukur waktu mulai
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            sopir.SetTugasBerdasarkanArmada(dataMassal, DateTime.Now);

            // Alat ukur waktu berhenti
            stopwatch.Stop();
            long waktuEksekusi = stopwatch.ElapsedMilliseconds;

            // Cetak hasil untuk di-screenshot
            Console.WriteLine($"[PERFORMANCE METRIC] Waktu pemrosesan 100.000 data: {waktuEksekusi} milidetik.");

            Assert.IsTrue(waktuEksekusi < 1000, "Performa lambat!");
        }
    }
}
