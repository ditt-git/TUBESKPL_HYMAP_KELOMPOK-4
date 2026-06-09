using Microsoft.VisualStudio.TestTools.UnitTesting;
using HYMAPSOPIR;

namespace HYMAPSOPIR.Tests
{
    [TestClass]
    public class DatabaseSimulasiTests
    {
        [TestMethod]
        public void DataAwal_PelangganDanSopir_TidakBolehKosong()
        {
            // Act & Assert
            Assert.IsNotNull(DatabaseSimulasi.PelangganDB);
            Assert.IsTrue(DatabaseSimulasi.PelangganDB.Count > 0, "Database pelanggan harus memiliki isi awal.");

            Assert.IsNotNull(DatabaseSimulasi.SopirDB);
            Assert.IsTrue(DatabaseSimulasi.SopirDB.Count > 0, "Database sopir harus memiliki isi awal.");
        }
    }

﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using HYMAPSOPIR;

namespace HYMAPSOPIR.Tests
{
    [TestClass]
    public class DatabaseSimulasiTests
    {
        [TestMethod]
        public void DataAwal_PelangganDanSopir_TidakBolehKosong()
        {
            // Act & Assert
            Assert.IsNotNull(DatabaseSimulasi.PelangganDB);
            Assert.IsTrue(DatabaseSimulasi.PelangganDB.Count > 0, "Database pelanggan harus memiliki isi awal.");

            Assert.IsNotNull(DatabaseSimulasi.SopirDB);
            Assert.IsTrue(DatabaseSimulasi.SopirDB.Count > 0, "Database sopir harus memiliki isi awal.");
        }
    }
}