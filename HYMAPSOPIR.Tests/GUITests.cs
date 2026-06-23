using Microsoft.VisualStudio.TestTools.UnitTesting;
using HYMAPSOPIR;
using System;

namespace HYMAPSOPIR.Tests
{
    [TestClass]
    public class GUITests
    {
        [TestMethod]
        public void FormLogin_BisaDiinisialisasiTanpaCrash()
        {
            // Act
            using (FormLogin form = new FormLogin())
            {
                // Assert
                Assert.IsNotNull(form);
            }
        }

        [TestMethod]
        public void DetailPengiriman_BisaDiinisialisasiTanpaCrash()
        {
            // Arrange
            Pelanggan dummyPelanggan = new Pelanggan("P1", "Test", "Alamat", Wilayah.Denpasar, DateTime.Now, 0);

            Pengiriman dummyTugas = new Pengiriman(dummyPelanggan, DateTime.Now, 1);

            // Act
            using (DetailPengiriman form = new DetailPengiriman(dummyTugas))
            {
                // Assert
                Assert.IsNotNull(form);
            }
        }
    }
}