using Microsoft.VisualStudio.TestTools.UnitTesting;
using HYMAPSOPIR;
using System;

namespace HYMAPSOPIR.Tests
{
    [TestClass]
    public class PrioritasCheckerTests
    {
        [TestMethod]
        public void HitungPrioritas_TepatWaktu_MengembalikanNormal()
        {
            // Arrange
            DateTime jadwal = new DateTime(2026, 5, 20);
            DateTime hariIni = new DateTime(2026, 5, 20); // 0 hari telat

            // Act
            PrioritasPengiriman hasil = PrioritasChecker.HitungPrioritas(jadwal, hariIni);

            // Assert
            Assert.AreEqual(PrioritasPengiriman.Normal, hasil);
        }

        [TestMethod]
        public void HitungPrioritas_Telat1Hari_MengembalikanTerlambat()
        {
            // Arrange
            DateTime jadwal = new DateTime(2026, 5, 19);
            DateTime hariIni = new DateTime(2026, 5, 20); // 1 hari telat

            // Act
            PrioritasPengiriman hasil = PrioritasChecker.HitungPrioritas(jadwal, hariIni);

            // Assert
            Assert.AreEqual(PrioritasPengiriman.Terlambat, hasil);
        }

        [TestMethod]
        public void HitungPrioritas_Telat3Hari_MengembalikanTerlambat()
        {
            // Arrange
            DateTime jadwal = new DateTime(2026, 5, 17);
            DateTime hariIni = new DateTime(2026, 5, 20); // 3 hari telat

            // Act
            PrioritasPengiriman hasil = PrioritasChecker.HitungPrioritas(jadwal, hariIni);

            // Assert
            Assert.AreEqual(PrioritasPengiriman.Terlambat, hasil);
        }

        [TestMethod]
        public void HitungPrioritas_Telat4Hari_MengembalikanDarurat()
        {
            // Arrange
            DateTime jadwal = new DateTime(2026, 5, 16);
            DateTime hariIni = new DateTime(2026, 5, 20); // 4 hari telat

            // Act
            PrioritasPengiriman hasil = PrioritasChecker.HitungPrioritas(jadwal, hariIni);

            // Assert
            Assert.AreEqual(PrioritasPengiriman.Darurat, hasil);
        }

        [TestMethod]
        public void HitungPrioritas_BelumJadwalnya_MengembalikanNormal()
        {
            // Arrange
            DateTime jadwal = new DateTime(2026, 5, 21);
            DateTime hariIni = new DateTime(2026, 5, 20); // -1 hari telat

            // Act
            PrioritasPengiriman hasil = PrioritasChecker.HitungPrioritas(jadwal, hariIni);

            // Assert
            Assert.AreEqual(PrioritasPengiriman.Normal, hasil);
        }
    }
}
