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

        public string LogMessage => $"Memperbarui status pengiriman (Pelanggan ID: {_tugasPengiriman?.DataPelanggan?.IdPelanggan}) menjadi {_statusKirimBaru}";

        public bool Execute()
        {
            // DESIGN BY CONTRACT
            if (_galonKembali < 0)
            {
                throw new InvalidOperationException("KONTRAK: Galon kembali tidak boleh negatif!");
            }

            if (_statusKirimBaru == (StatusPengiriman)0 || _statusKirimBaru == (StatusPengiriman)2) // Belum Terkirim atau Gagal
            {
                // Memastikan status bayar harus Bon
                Debug.Assert(_statusBayarBaru == StatusPembayaran.Bon,
                    "KONTRAK: Belum terkirim atau gagal tidak dapat pilih pembayaran selain bon!");

                if (_statusBayarBaru != StatusPembayaran.Bon)
                {
                    throw new InvalidOperationException("KONTRAK: Belum terkirim atau gagal tidak dapat pilih pembayaran selain bon!");
                }

                if (_galonKembali > 0)
                {
                    throw new InvalidOperationException("KONTRAK: Belum terkirim atau gagal tidak dapat mengembalikan galon!");
                }
            }

            _tugasPengiriman.StatusKirim = _statusKirimBaru;
            _tugasPengiriman.StatusBayar = _statusBayarBaru;
            _tugasPengiriman.GalonKembali = _galonKembali;

            Library.Database.PengirimanDAO.SimpanJadwalPengiriman(_tugasPengiriman);

            // Jika status pengiriman adalah "SudahTerkirim", perbarui TanggalTerakhirKirim untuk siklus langganan 7 hari
            if (_statusKirimBaru == StatusPengiriman.SudahTerkirim)
            {
                if (_tugasPengiriman.TanggalTugas >= _tugasPengiriman.DataPelanggan.TanggalTerakhirKirim)
                {
                    _tugasPengiriman.DataPelanggan.UpdateTanggalPengirimanBerhasil(_tugasPengiriman.TanggalTugas);
                    Library.Database.PengirimanDAO.UpdateTanggalTerakhirKirim(_tugasPengiriman.DataPelanggan.IdPelanggan, _tugasPengiriman.TanggalTugas);
                }
            }

            return true;
        }
    }
}
