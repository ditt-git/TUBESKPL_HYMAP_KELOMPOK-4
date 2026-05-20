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
            Armada armadaUji = Armada.Denpasar;

            // Act (Aksi)
            string[] hasil = RouteTable.GetRute(armadaUji);

            // Assert (Validasi)
            Assert.IsNotNull(hasil);
            Assert.AreEqual(3, hasil.Length);
            Assert.AreEqual("P001", hasil[0]); // P001 harus ada di Denpasar
        }
    }
}