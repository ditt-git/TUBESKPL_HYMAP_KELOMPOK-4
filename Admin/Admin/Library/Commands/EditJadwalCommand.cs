using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class EditJadwalCommand : ICommand
    {
        private int _idPengiriman;
        private DateTime _tanggal;
        private int _idPelanggan;
        private int _idUser;
        private int _jumlahPesanan;

        public EditJadwalCommand(int idPengiriman, DateTime tanggal, int idPelanggan, int idUser, int jumlahPesanan)
        {
            _idPengiriman = idPengiriman;
            _tanggal = tanggal;
            _idPelanggan = idPelanggan;
            _idUser = idUser;
            _jumlahPesanan = jumlahPesanan;
        }

        public string LogMessage => $"Mengedit jadwal pengiriman ID: {_idPengiriman} (Pelanggan ID: {_idPelanggan}, Sopir ID: {_idUser})";

        public bool Execute()
        {
            return AdminDAO.EditJadwal(_idPengiriman, _tanggal, _idPelanggan, _idUser, _jumlahPesanan);
        }
    }
}
