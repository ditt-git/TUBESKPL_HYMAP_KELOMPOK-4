using System;
using System.Collections.Generic;
using System.Text;

namespace HYMAPSOPIR
{
    public static class PrioritasChecker
    {
        // Enum untuk Trigger/Event transisi prioritas
        public enum TriggerPrioritas
        {
            TepatWaktu,          // Hari telat <= 0
            Telat,   // Hari telat 1-3 hari
            Darurat // Hari telat >= 4 hari
        }

        // Tabel Transisi State-Based dengan index 2D: [CurrentState, Trigger] -> NextState
        // Row index: PrioritasPengiriman (Normal = 0, Terlambat = 1, Darurat = 2)
        // Column index: TriggerPrioritas (TepatWaktu = 0, Telat 1-3 Hari = 1, Telat > 4 Hari = 2)
        private static readonly PrioritasPengiriman[,] TabelTransisi = new PrioritasPengiriman[3, 3]
        {
            // TepatWaktu                   Telat1Sampai3Hari               Telat4HariAtauLebih
            { PrioritasPengiriman.Normal,   PrioritasPengiriman.Terlambat,  PrioritasPengiriman.Darurat }, // State: Normal
            { PrioritasPengiriman.Normal,   PrioritasPengiriman.Terlambat,  PrioritasPengiriman.Darurat }, // State: Terlambat
            { PrioritasPengiriman.Normal,   PrioritasPengiriman.Terlambat,  PrioritasPengiriman.Darurat }  // State: Darurat
        };


        public static PrioritasPengiriman HitungPrioritas(DateTime jadwalSeharusnya, DateTime tanggalHariIni)
        {
            int hariTelat = (tanggalHariIni - jadwalSeharusnya).Days;

            // Tentukan trigger/index berdasarkan hari keterlambatan
            TriggerPrioritas trigger;
            if (hariTelat <= 0)
            {
                trigger = TriggerPrioritas.TepatWaktu;
            }
            else if (hariTelat >= 1 && hariTelat <= 3)
            {
                trigger = TriggerPrioritas.Telat;
            }
            else
            {
                trigger = TriggerPrioritas.Darurat;
            }

            // State awal (default)
            PrioritasPengiriman currentState = PrioritasPengiriman.Normal;

            // Transisi ke state berikutnya menggunakan tabel transisi berbasis index
            PrioritasPengiriman nextState = TabelTransisi[(int)currentState, (int)trigger];

            return nextState;
        }
    }
}

