using System;
using System.Collections.Generic;
using System.Linq;

namespace HYMAPSOPIR
{
   
    public class JadwalService
    {
        public void CekDanGenerateJadwalHariIni(Sopir sopir, DateTime tanggalPilih)
        {
            Library.Database.SopirDAO.GenerateJadwalSopir(sopir.Username, sopir.ArmadaTugas, tanggalPilih, sopir.IdUserDb);
        }

        public void SetTugasBerdasarkanArmada(Sopir sopir, List<Pelanggan> semuaPelanggan, DateTime hariIni)
        {
            if (semuaPelanggan == null) return;

            string[] ruteId = RouteTable.GetRute(sopir.ArmadaTugas) ?? new string[0];
            bool isHariIni = hariIni.Date == DateTime.Today;

            var pelangganTarget = semuaPelanggan
                .Where(p => p != null &&
                            ruteId.Contains(p.IdPelanggan) &&
                            (
                                p.JadwalBerikutnya().Date <= hariIni.Date ||
                                p.TanggalTerakhirKirim.Date == hariIni.Date ||
                                (sopir.DaftarTugasHariIni != null && sopir.DaftarTugasHariIni.Any(t => t.DataPelanggan.IdPelanggan == p.IdPelanggan && t.TanggalTugas.Date == hariIni.Date))
                            ))
                .ToList();

            var tugasBaruAtauLama = new List<Pengiriman>();

            foreach (var p in pelangganTarget)
            {
                var tugasSudahAda = sopir.DaftarTugasHariIni?.Find(t => t.DataPelanggan.IdPelanggan == p.IdPelanggan && t.TanggalTugas.Date == hariIni.Date);

                if (tugasSudahAda != null)
                {
                    tugasBaruAtauLama.Add(tugasSudahAda);
                }
                else
                {
                    Pengiriman tugasBaru = new Pengiriman(p, hariIni, sopir.IdUserDb);
                    tugasBaru.Prioritas = PrioritasChecker.HitungPrioritas(p.JadwalBerikutnya(), hariIni);

                    if (Library.Database.PengirimanDAO.CekStatusLaporan(p.IdPelanggan, hariIni, out StatusPengiriman stKirim, out StatusPembayaran stBayar))
                    {
                        tugasBaru.StatusKirim = stKirim;
                        tugasBaru.StatusBayar = stBayar;
                    }
                    tugasBaruAtauLama.Add(tugasBaru);
                }
            }
            sopir.DaftarTugasHariIni = tugasBaruAtauLama.OrderByDescending(t => t.Prioritas).ToList();
        }

        public Pengiriman AmbilTugasBerdasarkanNomor(Sopir sopir, int nomor)
        {
            if (sopir.DaftarTugasHariIni != null && nomor > 0 && nomor <= sopir.DaftarTugasHariIni.Count)
            {
                return sopir.DaftarTugasHariIni[nomor - 1];
            }
            return null;
        }
    }
}