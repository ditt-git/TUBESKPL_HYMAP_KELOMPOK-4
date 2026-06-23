using Microsoft.VisualStudio.TestTools.UnitTesting;
using HYMAPSOPIR;
using System.Collections.Generic;

namespace HYMAPSOPIR.Tests
{
    [TestClass]
    public class DataHelperTests
    {
        [TestMethod]
        public void CariBerdasarkanId_DataDitemukan_MengembalikanObjekYangBenar()
        {
            // Arrange
            var daftarSopir = new List<Sopir>
            {
                new Sopir(1, "Budi", "budi123", "pass", Wilayah.Gianyar)
            };

            // Act
            Sopir hasil = DataHelper.CariBerdasarkanId(daftarSopir, "budi123");

            // Assert
            Assert.IsNotNull(hasil);
            Assert.AreEqual("Budi", hasil.Nama);
        }

        [TestMethod]
        public void CariBerdasarkanId_DataTidakAda_MengembalikanNull()
        {
            // Arrange
            var daftarSopir = new List<Sopir>(); // List kosong

            // Act
            Sopir hasil = DataHelper.CariBerdasarkanId(daftarSopir, "joko99");

            // Assert
            Assert.IsNull(hasil); // null karena tidak ketemu
        }
    }
}