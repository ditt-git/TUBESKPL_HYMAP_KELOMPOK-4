using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HYMAPSOPIR
{

    public static class RouteTable
    {
        // Index 0: Denpasar, 1: Karangasem, 2: Gianyar, 3: Tabanan
        private static readonly string[][] TabelRute = new string[][]
    {
            new string[] { "P001", "P004", "P007" }, // Denpasar
            new string[] { "P002", "P005" },         // Karangasem
            new string[] { "P003", "P006" },         // Gianyar
            new string[] { "P008", "P009" }          // Tabanan
    };

        public static string[] GetRute(Armada armada)
        {
            // menggunakan casting (int) armada sebagai index untuk mengambil datanya

            return TabelRute[(int)armada];
        }
    }
}