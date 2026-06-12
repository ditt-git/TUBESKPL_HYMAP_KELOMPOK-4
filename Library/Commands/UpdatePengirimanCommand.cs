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

        public UpdatePengirimanCommand(Pengiriman tugasPengiriman, StatusPengiriman statusKirimBaru, StatusPembayaran statusBayarBaru)
        {
            _tugasPengiriman = tugasPengiriman;
            _statusKirimBaru = statusKirimBaru;
            _statusBayarBaru = statusBayarBaru;
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

            if (_tugasPengiriman.StatusKirim == StatusPengiriman.SudahTerkirim)
            {
                _tugasPengiriman.DataPelanggan.UpdateTanggalPengirimanBerhasil(DateTime.Now);
            }

            Library.Database.PengirimanDAO.SimpanJadwalPengiriman(_tugasPengiriman);
        }
    }
}
