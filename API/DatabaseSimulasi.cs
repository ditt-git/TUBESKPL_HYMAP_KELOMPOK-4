using System;
using System.Collections.Generic;
using System.Text;

namespace HYMAPSOPIR
{
    public static class DatabaseSimulasi
    {
        public static List<Pelanggan> PelangganDB = new List<Pelanggan>
        {
            new Pelanggan("P001", "Tono", "Jl. Teuku Umar", Armada.Denpasar, new DateTime(2026, 5, 3)),
            new Pelanggan("P002", "Pak RT", "Amlapura", Armada.Karangasem, new DateTime(2026, 5, 2)),
            new Pelanggan("P003", "Budi", "Ubud", Armada.Gianyar, new DateTime(2026, 4, 30)),
            new Pelanggan("P004", "Siti", "Renon", Armada.Denpasar, new DateTime(2026, 4, 25)),
        };

        public static List<Sopir> SopirDB = new List<Sopir>
        {
            new Sopir("Aditya", "aditya", "12345", Armada.Denpasar),
            new Sopir("Budi", "budi", "admin", Armada.Gianyar)
        };
    }
}
