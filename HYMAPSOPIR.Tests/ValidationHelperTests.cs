using Microsoft.VisualStudio.TestTools.UnitTesting;
using HYMAPSOPIR;

namespace HYMAPSOPIR.Tests
{
    [TestClass]
    public class ValidationHelperTests
    {
        [TestMethod]
        public void IsPasswordLengthValid_KurangDari5Karakter_MengembalikanFalse()
        {
            // Arrange & Act
            bool hasil = ValidationHelper.IsPasswordLengthValid("abcd"); // 4 karakter

            // Assert
            Assert.IsFalse(hasil);
        }

        [TestMethod]
        public void IsPasswordLengthValid_Tepat5Karakter_MengembalikanTrue()
        {
            // Arrange & Act
            bool hasil = ValidationHelper.IsPasswordLengthValid("abcde"); // 5 karakter

            // Assert
            Assert.IsTrue(hasil);
        }

        [TestMethod]
        public void IsPasswordLengthValid_LebihDari5Karakter_MengembalikanTrue()
        {
            // Arrange & Act
            bool hasil = ValidationHelper.IsPasswordLengthValid("abcdef"); // 6 karakter

            // Assert
            Assert.IsTrue(hasil);
        }

        [TestMethod]
        public void IsPasswordLengthValid_Null_MengembalikanFalse()
        {
            // Arrange & Act
            bool hasil = ValidationHelper.IsPasswordLengthValid(null!);

            // Assert
            Assert.IsFalse(hasil);
        }
    }
}
