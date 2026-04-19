using System;
using System.Collections.Generic;
using System.Text;

namespace HYMAPSOPIR
{
    public static class PrioritasChecker
    {
        // Implementasi Table-Driven dengan Array
        // Index merepresentasikan: Hari keterlambatan (0 = tepat waktu, 1 = telat 1 hari, dst)
        private static readonly PrioritasPengiriman[] TabelPrioritas = new PrioritasPengiriman[]
        {
            PrioritasPengiriman.Normal,       // Index 0: Tepat waktu
            PrioritasPengiriman.Terlambat,    // Index 1: Telat 1 hari
            PrioritasPengiriman.Terlambat,    // Index 2: Telat 2 hari
            PrioritasPengiriman.Terlambat,    // Index 3: Telat 3 hari
            PrioritasPengiriman.Darurat       // Index 4: Telat 4 hari (dan seterusnya)
        };

        public static PrioritasPengiriman HitungPrioritas(DateTime jadwalSeharusnya, DateTime tanggalHariIni)
        {
            int hariTelat = (tanggalHariIni - jadwalSeharusnya).Days;

            if (hariTelat <= 0) return PrioritasPengiriman.Normal; // Belum telat
            if (hariTelat >= TabelPrioritas.Length) return PrioritasPengiriman.Darurat; // Sangat telat

            return TabelPrioritas[hariTelat];
        }
    }
}
