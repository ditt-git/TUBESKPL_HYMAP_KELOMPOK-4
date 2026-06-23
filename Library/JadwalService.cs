using System;
using System.Collections.Generic;
using System.Linq;

namespace HYMAPSOPIR
{
   
    public class JadwalService
    {
        public void CekDanGenerateJadwalHariIni(Sopir sopir, DateTime tanggalPilih)
        {
            Library.Database.SopirDAO.GenerateJadwalSopir(sopir.Username, sopir.WilayahTugas, tanggalPilih, sopir.IdUserDb);
        }

        public void SetTugasBerdasarkanWilayah(Sopir sopir, List<Pelanggan> semuaPelanggan, DateTime hariIni)
        {
            if (semuaPelanggan == null) return;

            List<string> pelangganDitugaskan = Library.Database.SopirDAO.GetJadwalPelangganSopir(sopir.IdUserDb, hariIni);

            string[] ruteId = RouteTable.GetRute(sopir.WilayahTugas, semuaPelanggan) ?? new string[0];
            bool isHariIni = hariIni.Date == DateTime.Today;

            var pelangganTarget = semuaPelanggan
                .Where(p => p != null &&
                            (
                                pelangganDitugaskan.Contains(p.IdPelanggan) 
                                ||
                                (ruteId.Contains(p.IdPelanggan) &&
                                 (
                                     p.JadwalBerikutnya().Date <= hariIni.Date ||
                                     p.TanggalTerakhirKirim.Date == hariIni.Date ||
                                     (sopir.DaftarTugasHariIni != null && sopir.DaftarTugasHariIni.Any(t => t.DataPelanggan.IdPelanggan == p.IdPelanggan && t.TanggalTugas.Date == hariIni.Date))
                                 ))
                            ))
                .ToList();

            var tugasBaruAtauLama = new List<Pengiriman>();

            foreach (var p in pelangganTarget)
            {
                var tugasSudahAda = sopir.DaftarTugasHariIni?.Find(t => t.DataPelanggan.IdPelanggan == p.IdPelanggan && t.TanggalTugas.Date == hariIni.Date);

                if (tugasSudahAda != null)
                {
                    if (Library.Database.PengirimanDAO.CekStatusLaporan(p.IdPelanggan, hariIni, out StatusPengiriman stKirim2, out StatusPembayaran stBayar2, out int jmlPesanan2, out int galonKembali2))
                    {
                        tugasSudahAda.StatusKirim = stKirim2;
                        tugasSudahAda.StatusBayar = stBayar2;
                        tugasSudahAda.JumlahPesanan = jmlPesanan2;
                        tugasSudahAda.GalonKembali = galonKembali2;
                    }
                    tugasBaruAtauLama.Add(tugasSudahAda);
                }
                else

                {
                    Pengiriman tugasBaru = new Pengiriman(p, hariIni, sopir.IdUserDb);
                    tugasBaru.Prioritas = PrioritasChecker.HitungPrioritas(p.JadwalBerikutnya(), hariIni);

                    if (Library.Database.PengirimanDAO.CekStatusLaporan(p.IdPelanggan, hariIni, out StatusPengiriman stKirim, out StatusPembayaran stBayar, out int jmlPesanan, out int galonKembali))
                    {
                        tugasBaru.StatusKirim = stKirim;
                        tugasBaru.StatusBayar = stBayar;
                        tugasBaru.JumlahPesanan = jmlPesanan;
                        tugasBaru.GalonKembali = galonKembali;
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