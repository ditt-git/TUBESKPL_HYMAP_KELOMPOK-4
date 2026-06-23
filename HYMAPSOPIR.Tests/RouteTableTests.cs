using Microsoft.VisualStudio.TestTools.UnitTesting;
using HYMAPSOPIR;

namespace HYMAPSOPIR.Tests
{
    [TestClass]
    public class RouteTableTests
    {
        [TestMethod]
        public void GetRute_ArmadaDenpasar_MengembalikanTigaPelanggan()
        {
            // Arrange (Persiapan)
            Wilayah armadaUji = Wilayah.Denpasar;
            System.Collections.Generic.List<Pelanggan> pelangganDb = new System.Collections.Generic.List<Pelanggan>
            {
                new Pelanggan("P001", "Pelanggan 1", "Alamat 1", Wilayah.Denpasar, System.DateTime.Now, 0),
                new Pelanggan("P002", "Pelanggan 2", "Alamat 2", Wilayah.Denpasar, System.DateTime.Now, 0),
                new Pelanggan("P003", "Pelanggan 3", "Alamat 3", Wilayah.Denpasar, System.DateTime.Now, 0),
                new Pelanggan("P004", "Pelanggan 4", "Alamat 4", Wilayah.Denpasar, System.DateTime.Now, 0),
                new Pelanggan("P005", "Pelanggan 5", "Alamat 5", Wilayah.Denpasar, System.DateTime.Now, 0),
                new Pelanggan("P006", "Pelanggan 6", "Alamat 6", Wilayah.Denpasar, System.DateTime.Now, 0),
                new Pelanggan("P007", "Pelanggan 7", "Alamat 7", Wilayah.Denpasar, System.DateTime.Now, 0),
                new Pelanggan("P008", "Pelanggan 8", "Alamat 8", Wilayah.Denpasar, System.DateTime.Now, 0),
            };

            // Act (Aksi)
            string[] hasil = RouteTable.GetRute(armadaUji, pelangganDb);

            // Assert (Validasi)
            Assert.IsNotNull(hasil);
            Assert.IsTrue(hasil.Length >= 8, "Harus mengembalikan setidaknya 8 pelanggan awal (bisa lebih jika ada yang baru ditambah).");
            Assert.IsTrue(System.Linq.Enumerable.Contains(hasil, "P001"), "P001 harus ada di Denpasar");
        }
    }
}