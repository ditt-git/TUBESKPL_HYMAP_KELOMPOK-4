using System;
using System.Diagnostics;
using HYMAPSOPIR;

namespace Library.Commands
{
    public class UpdatePengirimanCommand : ICommand
    {
        private readonly Pengiriman _tugasPengiriman;
        private readonly StatusPengiriman _statusKirimBaru;
        private readonly StatusPembayaran _statusBayarBaru;
        private readonly int _galonKembali; 

        public UpdatePengirimanCommand(Pengiriman tugasPengiriman, StatusPengiriman statusKirimBaru, StatusPembayaran statusBayarBaru, int galonKembali)
        {
            _tugasPengiriman = tugasPengiriman;
            _statusKirimBaru = statusKirimBaru;
            _statusBayarBaru = statusBayarBaru;
            _galonKembali = galonKembali;

        }

        public void Execute()
        {
            // DESIGN BY CONTRACT
            if (_statusKirimBaru == (StatusPengiriman)0 || _statusKirimBaru == (StatusPengiriman)2) // Belum Terkirim atau Gagal
            {
                // Memastikan status bayar harus Bon
                Debug.Assert(_statusBayarBaru == StatusPembayaran.Bon,
                    "KONTRAK: Belum terkirim atau gagal tidak dapat pilih pembayaran selain bon!");

                if (_statusBayarBaru != StatusPembayaran.Bon)
                {
                    throw new InvalidOperationException("KONTRAK: Belum terkirim atau gagal tidak dapat pilih pembayaran selain bon!");
                }
            }

            _tugasPengiriman.StatusKirim = _statusKirimBaru;
            _tugasPengiriman.StatusBayar = _statusBayarBaru;
            _tugasPengiriman.GalonKembali = _galonKembali;

            if (_tugasPengiriman.StatusKirim == StatusPengiriman.SudahTerkirim)
            {
                // Gunakan tanggal tugas, bukan DateTime.Now, agar jadwal tidak kacau
                DateTime tanggalKirim = _tugasPengiriman.TanggalTugas;
                if (tanggalKirim >= _tugasPengiriman.DataPelanggan.TanggalTerakhirKirim)
                {
                    _tugasPengiriman.DataPelanggan.UpdateTanggalPengirimanBerhasil(tanggalKirim);
                    // Simpan juga ke database agar persisten setelah logout
                    Library.Database.PengirimanDAO.UpdateTanggalTerakhirKirim(
                        _tugasPengiriman.DataPelanggan.IdPelanggan, tanggalKirim);
                }
            }

            Library.Database.PengirimanDAO.SimpanJadwalPengiriman(_tugasPengiriman);
        }
    }
}
